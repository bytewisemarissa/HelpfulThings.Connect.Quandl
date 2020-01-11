using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HelpfulThings.Connect.Quandl.Exceptions;
using HelpfulThings.Connect.Quandl.Models.Shared;
using HelpfulThings.Connect.Quandl.Statistics;
using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl
{
    public abstract class QuandlRequest
    {
        public static string ApiKey { private get; set; }
        public static string VersionKey { private get; set; }

        private const string BuildVersionKey = "2015-04-09";

        public QuandlRequest()
        {
            VersionKey = "2015-04-09";
        }

        protected static async Task<Stream> GetStreamAsync(HttpClient client, string requestString, bool useApiKey = true)
        {
            if (useApiKey)
            {
                requestString = AppendApiKeyAndVersionParameters(requestString);
            }

            HttpResponseMessage responseMessage = await client.GetAsync(requestString);

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
                    throw new AbnormalHttpStatusCodeException($"Quandle has returned the HTTP status code {responseMessage.StatusCode}");
                }
            }

            return await responseMessage.Content.ReadAsStreamAsync();
        }

        private static void ProcessHeaders(HttpResponseHeaders headers)
        {
            RequestStatistic stats = new RequestStatistic();

            if (headers.Date.HasValue)
            {
                stats.Date = headers.Date.Value;
            }

            if (headers.Contains("X-RateLimit-Limit"))
            {
                stats.CurrentRateLimit = Convert.ToInt32(headers.Single(header => header.Key == "X-RateLimit-Limit").Value.First());
            }

            if (headers.Contains("X-RateLimit-Remaining"))
            {
                stats.RateLimitRemaining = Convert.ToInt32(headers.Single(header => header.Key == "X-RateLimit-Remaining").Value.First());
            }

            if (headers.Contains("X-Request-ID"))
            {
                stats.RequestId = Guid.Parse(headers.Single(header => header.Key == "X-Request-ID").Value.First());
            }

            if (headers.Contains("X-Runtime"))
            {
                stats.RequestRunTime = Convert.ToDouble(headers.Single(header => header.Key == "X-Runtime").Value.First());
            }

            StatisticsCollector.CollectRequestStatistic(stats);
        }

        protected static async Task<string> GetStringAsync(HttpClient client, string requestString)
        {
            Stream responseStream = await GetStreamAsync(client, requestString);
            
            using (StreamReader reader = new StreamReader(responseStream))
            {
                return reader.ReadToEnd();
            }
        }

        private static string AppendApiKeyAndVersionParameters(string requestUri)
        {
            string transformedString = null;
            if (!requestUri.Contains('?'))
            {
                transformedString = String.Concat(requestUri, $"?api_key={ApiKey}");
            }
            else
            {
                transformedString = String.Concat(requestUri, $"&api_key={ApiKey}");
            }

            if (VersionKey != BuildVersionKey)
            {
                transformedString = String.Concat(transformedString, $"&api_version={BuildVersionKey}");
            }

            return transformedString;
        }
    }
}
