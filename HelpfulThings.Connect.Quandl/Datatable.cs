using System.Collections.Generic;
using System.Threading.Tasks;
using HelpfulThings.Connect.Quandl.Models.Datatable;

namespace HelpfulThings.Connect.Quandl
{
    public interface IDatatable
    {
        Task<Response> GetEntireDatatable(
            string complexShortCode, 
            string cursor);

        Task<Response> GetEntireDatatable(
            string databaseShortCode, 
            string datatableShortCode, 
            string cursor);

        Task<Response> GetFilteredDatatable(
            string complexShortCode,
            Dictionary<string, List<string>> rowSearchFilters,
            List<string> columnSearchFilters,
            string cursor);
        
        Task<Response> GetFilteredDatatable(
            string databaseShortCode,
            string datatableShortCode,
            Dictionary<string, List<string>> rowSearchFilters, 
            List<string> columnSearchFilters,  
            string cursor);
    }

    public class Datatable : IDatatable
    {
        private IRequestRouter _requestRouter;
        
        public Datatable(IRequestRouter requestRouter)
        {
            _requestRouter = requestRouter;
        }

        public async Task<Response> GetEntireDatatable(
            string complexShortCode, 
            string cursor)
        {
            var splitCode = complexShortCode.SplitComplexCode();

            return await GetEntireDatatable(
                splitCode[0], 
                splitCode[1], 
                cursor);
        }
        
        public async Task<Response> GetEntireDatatable(
            string databaseShortCode, 
            string datatableShortCode, 
            string cursor)
        {
            var pathReplacements = new List<string>()
            {
                databaseShortCode,
                datatableShortCode
            };
            
            var parameters = new Dictionary<string, string>();
            
            if (cursor != null)
            {
                parameters.Add("qopts.cursor_id", cursor);
            }

            return await _requestRouter.MakeRequest<Response>(
                Endpoints.Datatable.GetDatatable,
                parameters,
                pathReplacements);
        }

        public async Task<Response> GetFilteredDatatable(
            string complexShortCode,
            Dictionary<string, List<string>> rowSearchFilters,
            List<string> columnSearchFilters,
            string cursor)
        {
            var splitCodes = complexShortCode.SplitComplexCode();
            
            return await GetFilteredDatatable(
                splitCodes[0], 
                splitCodes[1],
                rowSearchFilters,
                columnSearchFilters, 
                cursor);
        }
        
        public async Task<Response> GetFilteredDatatable(
            string databaseShortCode,
            string datatableShortCode,
            Dictionary<string, List<string>> rowSearchFilters, 
            List<string> columnSearchFilters,  
            string cursor)
        {
            var pathReplacements = new List<string>()
            {
                databaseShortCode,
                datatableShortCode
            };

            var parameters = new Dictionary<string, string>();
            
            if (cursor != null)
            {
                parameters.Add("qopts.cursor_id", cursor);
            }
            
            if (rowSearchFilters != null)
            {
                foreach (var rowSearchFilter in rowSearchFilters)
                {
                    parameters.Add(rowSearchFilter.Key, string.Join(',', rowSearchFilter.Value));    
                }
            }

            if (columnSearchFilters != null)
            {
                parameters.Add("qopts.columns", string.Join(",", columnSearchFilters));
            }

            return await _requestRouter.MakeRequest<Response>(
                Endpoints.Datatable.GetDatatable,
                parameters,
                pathReplacements);
        }
    }
}
