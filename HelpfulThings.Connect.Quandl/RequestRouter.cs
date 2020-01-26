using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HelpfulThings.Connect.Quandl.Exceptions;
using HelpfulThings.Connect.Quandl.Ioc;
using HelpfulThings.Connect.Quandl.Models.Shared;
using HelpfulThings.Connect.Quandl.Statistics;
using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl
{
    public interface IRequestRouter
    {
        Task<T> MakeRequest<T>(
            string relativeUri, 
            Dictionary<string, string> parameters = null,
            List<string> pathReplacements = null,
            bool includeKey = true);

        Task<Stream> MakeRequestStream(
            string relativeUri,
            Dictionary<string, string> parameters = null,
            List<string> pathReplacements = null,
            bool includeKey = true);
    }

    public class RequestRouter : IRequestRouter
    {
        private readonly QuandlApiOptions _options;
        private readonly IStatisticsCollector _statisticsCollector;
        private readonly HttpClient _client;

        private const string BuildVersionKey = "2015-04-09";

        public RequestRouter(QuandlApiOptions options, IStatisticsCollector statisticsCollector)
        {
            _options = options;
            _statisticsCollector = statisticsCollector;
            
            _client = new HttpClient()
            {
                BaseAddress = new Uri(Endpoints.QuandlApiBase)
            };
        }

        public async Task<T> MakeRequest<T>(
            string relativeUri, 
            Dictionary<string, string> parameters = null,
            List<string> pathReplacements = null,
            bool includeKey = true)
        {
            string stringifiedResponse;
            using (var responseStream = await MakeRequestStream(relativeUri, parameters, pathReplacements, includeKey))
            {
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    stringifiedResponse = reader.ReadToEnd();
                }
            }

            return JsonConvert.DeserializeObject<T>(stringifiedResponse);
        }
        
        public async Task<Stream> MakeRequestStream(
            string relativeUri, 
            Dictionary<string, string> parameters = null,
            List<string> pathReplacements = null,
            bool includeKey = true)
        {
            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    queryString[parameter.Key] = parameter.Value;
                }
            }

            if (includeKey)
            {
                queryString["api_key"] = _options.ApiKey;
            }

            queryString["api_version"] = BuildVersionKey;

            var processedUri = relativeUri;
            
            if (pathReplacements != null)
            {
                processedUri = string.Format(relativeUri, pathReplacements.ToArray());
            }
            
            var requestUriBuilder = new UriBuilder(string.Concat(Endpoints.QuandlApiBase, processedUri));
            requestUriBuilder.Query = queryString.ToString();

            return await ProcessResponse(new Uri(requestUriBuilder.ToString()));
        }

        private async Task<Stream> ProcessResponse(Uri requestUri)
        {
            HttpResponseMessage responseMessage = await _client.GetAsync(requestUri);

            ProcessHeaders(responseMessage.Headers);

            if (responseMessage.StatusCode != HttpStatusCode.OK &&
                responseMessage.StatusCode != HttpStatusCode.Created &&
                responseMessage.StatusCode != HttpStatusCode.Accepted &&
                responseMessage.StatusCode != HttpStatusCode.NonAuthoritativeInformation &&
                responseMessage.StatusCode != HttpStatusCode.NoContent &&
                responseMessage.StatusCode != HttpStatusCode.ResetContent &&
                responseMessage.StatusCode != HttpStatusCode.PartialContent)
            {
                if (responseMessage.StatusCode != HttpStatusCode.BadRequest ||
                    responseMessage.StatusCode != HttpStatusCode.Unauthorized ||
                    responseMessage.StatusCode != HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode != HttpStatusCode.NotFound ||
                    responseMessage.StatusCode != HttpStatusCode.NotAcceptable ||
                    responseMessage.StatusCode != HttpStatusCode.RequestEntityTooLarge ||
                    responseMessage.StatusCode != HttpStatusCode.RequestUriTooLong ||
                    responseMessage.StatusCode != HttpStatusCode.InternalServerError ||
                    responseMessage.StatusCode != HttpStatusCode.BadGateway ||
                    responseMessage.StatusCode != HttpStatusCode.ServiceUnavailable)
                {
                    string errorContent = await responseMessage.Content.ReadAsStringAsync();
                    Exception newException;
                    try
                    {
                        ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(errorContent);
                        newException = new QuandlErrorException(error.Error.ErrorCode, error.Error.Message);
                    }
                    catch (Exception)
                    {
                        throw new QuandlErrorException("", errorContent);
                    }

                    throw newException;
                }
                else
                {
                    throw new AbnormalHttpStatusCodeException(
                        $"Quandle has returned the HTTP status code {responseMessage.StatusCode}");
                }
            }

            return await responseMessage.Content.ReadAsStreamAsync();
        }

        private void ProcessHeaders(HttpResponseHeaders headers)
        {
            var currentRateLimit =
                Convert.ToInt32(headers.Single(header => header.Key == "X-RateLimit-Limit").Value.First());
            
            var rateLimitRemaining = 
                Convert.ToInt32(headers.Single(header => header.Key == "X-RateLimit-Remaining").Value.First());
            
            var requestRunTime = 
                Convert.ToDouble(headers.Single(header => header.Key == "X-Runtime").Value.First());

            if (headers.Date == null) return;
            
            _statisticsCollector.CollectRequestStatistics(
                currentRateLimit, 
                rateLimitRemaining, 
                requestRunTime,
                headers.Date.Value);
        }
    }
}
