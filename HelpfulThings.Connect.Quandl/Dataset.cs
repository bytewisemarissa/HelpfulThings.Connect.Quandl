using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using HelpfulThings.Connect.Quandl.Models.Dataset;

namespace HelpfulThings.Connect.Quandl
{
    public interface IDataset
    {
        Task<OnlyData> GetDatasetDataOnly(string complexShortCode);

        Task<OnlyData> GetDatasetDataOnly(
            string databaseShortCode, 
            string datasetShortCode);

        Task<OnlyMetadata> GetDatasetMetadataOnly(string complexShortCode);

        Task<OnlyMetadata> GetDatasetMetadataOnly(
            string databaseShortCode, 
            string datasetShortCode);

        Task<Complete> GetDatasetComplete(
            string complexShortCode, 
            FilterOptions options = null);

        Task<Complete> GetDatasetComplete(
            string databaseShortCode, 
            string datasetShortCode, 
            FilterOptions options = null);

        Task<SearchResult> GetDatasetSearchResult(
            IEnumerable<string> searchTerms, 
            string databaseCode, 
            int resultsPerPage = 100, 
            int page = 1);
    }

    public class Dataset : IDataset
    {
        private readonly IRequestRouter _requestRouter;
        public Dataset(IRequestRouter requestRouter)
        {
            _requestRouter = requestRouter;
        }

        public async Task<OnlyData> GetDatasetDataOnly(string complexShortCode)
        {
            var splitCodes = complexShortCode.SplitComplexCode();

            return await GetDatasetDataOnly(splitCodes[0], splitCodes[1]);
        }

        public async Task<OnlyData> GetDatasetDataOnly(
            string databaseShortCode, 
            string datasetShortCode)
        {
            var pathReplacements = new List<string>()
            {
                databaseShortCode,
                datasetShortCode
            };

            var response = await _requestRouter.MakeRequest<OnlyDataResponse>(
                Endpoints.Dataset.DataOnly,
                null,
                pathReplacements);

            return response.OnlyData;
        }
        
        public async Task<OnlyMetadata> GetDatasetMetadataOnly(string complexShortCode)
        {
            var splitCodes = complexShortCode.SplitComplexCode();

            return await GetDatasetMetadataOnly(splitCodes[0], splitCodes[1]);
        }


        public async Task<OnlyMetadata> GetDatasetMetadataOnly(
            string databaseShortCode, 
            string datasetShortCode)
        {
            var pathReplacements = new List<string>()
            {
                databaseShortCode,
                datasetShortCode
            };


            var response = await _requestRouter.MakeRequest<OnlyMetadataResponse>(
                Endpoints.Dataset.MetadataOnly,
                null,
                pathReplacements);

            return response.MetadataOnly;
        }

        public async Task<Complete> GetDatasetComplete(
            string complexShortCode, 
            FilterOptions options = null)
        {
            var splitCodes = complexShortCode.SplitComplexCode();

            return await GetDatasetComplete(splitCodes[0], splitCodes[1], options);
        }
        
        public async Task<Complete> GetDatasetComplete(
            string databaseShortCode, 
            string datasetShortCode, 
            FilterOptions options = null)
        {
            var parameters = new Dictionary<string, string>();
            
            if (options != null)
            {
                ProcessFilterOptions(options, parameters);
            }
            
            var pathReplacements = new List<string>()
            {
                databaseShortCode,
                datasetShortCode
            };

            var response = await _requestRouter.MakeRequest<CompleteResponse>(
                Endpoints.Dataset.Complete, 
                parameters, 
                pathReplacements);

            return response.Complete;
        }

        public async Task<SearchResult> GetDatasetSearchResult(
            IEnumerable<string> searchTerms, 
            string databaseCode, 
            int resultsPerPage = 100, 
            int page = 1)
        {
            var encodedSearchTerms = WebUtility.HtmlEncode(string.Join(" ", searchTerms));
            
            var parameters = new Dictionary<string, string>
            {
                {"query", encodedSearchTerms },
                {"database_code", databaseCode},
                {"per_page", resultsPerPage.ToString()},
                {"page", page.ToString()}
            };

            return await _requestRouter.MakeRequest<SearchResult>(
                Endpoints.Dataset.DatasetSearch,
                parameters);
        }

        private static void ProcessFilterOptions(
            FilterOptions options, 
            IDictionary<string, string> parameterValues)
        {
            var filterAdditions = new List<string>();

            if (options.Collapse != null)
            {
                parameterValues.Add("collapse", options.Collapse.QueryStringValue);
            }

            if (options.Order != null)
            {
                parameterValues.Add("order", options.Order.QueryStringValue);
            }

            if (options.Transform != null)
            {
                parameterValues.Add("transform", options.Transform.QueryStringValue);
            }

            if (options.ColumnIndex.HasValue)
            {
                parameterValues.Add("column_index", options.ColumnIndex.Value.ToString());
            }

            if (options.Limit.HasValue)
            {
                parameterValues.Add("limit", options.Limit.Value.ToString());
            }

            if (options.StartDate.HasValue)
            {
                parameterValues.Add("start_date", options.StartDate.Value.ToString("yyyy-MM-dd"));
            }

            if (options.EndDate.HasValue)
            {
                parameterValues.Add("end_date", options.EndDate.Value.ToString("yyyy-MM-dd"));
            }
        }
    }
}
