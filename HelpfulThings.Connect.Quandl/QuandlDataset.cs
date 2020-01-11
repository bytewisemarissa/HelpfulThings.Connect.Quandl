using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HelpfulThings.Connect.Quandl.Exceptions;
using HelpfulThings.Connect.Quandl.Models.DatasetModels;
using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl
{
    public class QuandlDataset : QuandlRequest
    {
        private HttpClient Client => new HttpClient()
        {
            BaseAddress = new Uri("https://www.quandl.com/api/v3/datasets/")
        };

        private readonly string _databaseShortCode;
        private readonly string _datasetShortCode;

        public QuandlDataset(string combinedShortCode)
        {
            string[] splitShortCodes = combinedShortCode.Split('/');
            if (splitShortCodes.Length != 2)
            {
                throw new InvalidDatatableShortCodeCombinationException("The short code combination is not formated correctly.");
            }

            _databaseShortCode = splitShortCodes[0];
            _datasetShortCode = splitShortCodes[1];
        }

        public QuandlDataset(string databaseShortCode, string datasetShortCode) : base()
        {
            _databaseShortCode = databaseShortCode;
            _datasetShortCode = datasetShortCode;
        }

        public async Task<DatasetDataOnlyModel> GetDatasetDataOnly()
        {
            string datasetUrl = $"{_databaseShortCode}/{_datasetShortCode}/data.json";

            using (HttpClient client = Client)
            {
                string requestResult = await GetStringAsync(client, datasetUrl);
                DatasetOnlyDataResponse response = JsonConvert.DeserializeObject<DatasetOnlyDataResponse>(requestResult);
                return response.DatasetDataOnly;
            }
        }

        public async Task<DatasetOnlyMetadataModel> GetDatasetMetadataOnly()
        {
            string datasetUrl = $"{_databaseShortCode}/{_datasetShortCode}/metadata.json";

            using (HttpClient client = Client)
            {
                string requestResult = await GetStringAsync(client, datasetUrl);
                DatasetOnlyMetadataResponse response = JsonConvert.DeserializeObject<DatasetOnlyMetadataResponse>(requestResult);
                return response.DatasetMetadataOnly;
            }
        }

        public async Task<DatasetCompleteModel> GetDatasetComplete(DatasetFilterOptions options = null)
        {
            string datasetUrl = $"{_databaseShortCode}/{_datasetShortCode}.json";

            if (options != null)
            {
                datasetUrl = ProcessFilterOptions(datasetUrl, options);
            }

            using (HttpClient client = Client)
            {
                string requestResult = await GetStringAsync(client, datasetUrl);
                DatasetCompleteResponse response = JsonConvert.DeserializeObject<DatasetCompleteResponse>(requestResult);
                return response.CompleteDataset;
            }
        }

        public async Task<DatasetSearchResult> GetDatasetSearchResult(List<string> searchTerms, string databaseCode, int resultsPerPage = 100, int page = 1)
        {
            string compiledSearchTerms = WebUtility.HtmlEncode(String.Join("+", searchTerms));
            string datasetUrl = $"datasets.json?query={compiledSearchTerms}&database_code={databaseCode}&per_page={resultsPerPage}&page={page}";

            using (HttpClient client = new HttpClient() { BaseAddress = new Uri("https://www.quandl.com/api/v3/") })
            {
                string requestResult = await GetStringAsync(client, datasetUrl);
                DatasetSearchResult response = JsonConvert.DeserializeObject<DatasetSearchResult>(requestResult);
                return response;
            }
        }

        private string ProcessFilterOptions(string datatableUrl, DatasetFilterOptions options)
        {
            List<string> filterAdditions = new List<string>();

            if (options.Collapse != null)
            {
                filterAdditions.Add($"collapse={options.Collapse.QueryStringValue}");
            }

            if (options.Order != null)
            {
                filterAdditions.Add($"order={options.Order.QueryStringValue}");
            }

            if (options.Transform != null)
            {
                filterAdditions.Add($"transform={options.Transform.QueryStringValue}");
            }

            if (options.ColumnIndex.HasValue)
            {
                filterAdditions.Add($"column_index={options.ColumnIndex.Value}");
            }

            if (options.Limit.HasValue)
            {
                filterAdditions.Add($"limit={options.Limit.Value}");
            }

            if (options.StartDate.HasValue)
            {
                filterAdditions.Add($"start_date={options.StartDate.Value.ToString("yyyy-MM-dd")}");
            }

            if (options.EndDate.HasValue)
            {
                filterAdditions.Add($"end_date={options.EndDate.Value.ToString("yyyy-MM-dd")}");
            }

            if (filterAdditions.Count > 0)
            {
                datatableUrl = String.Concat(datatableUrl, "?");
            }

            return String.Concat(datatableUrl, String.Join("&", filterAdditions));
        }
    }
}
