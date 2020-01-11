using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HelpfulThings.Connect.Quandl.Exceptions;
using HelpfulThings.Connect.Quandl.Models.DatatableModels;
using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl
{
    public class QuandlDatatable : QuandlRequest
    {
        private HttpClient Client => new HttpClient()
        {
            BaseAddress = new Uri("https://www.quandl.com/api/v3/datatables/")
        };

        private readonly string _databaseShortCode;
        private readonly string _datatableShortCode;

        public QuandlDatatable(string combinedShortCode)
        {
            string[] splitShortCodes = combinedShortCode.Split('/');
            if (splitShortCodes.Length != 2)
            {
                throw new InvalidDatatableShortCodeCombinationException("The short code combination is not formated correctly.");
            }

            _databaseShortCode = splitShortCodes[0];
            _datatableShortCode = splitShortCodes[1];
        }

        public QuandlDatatable(string databaseShortCode, string datatableShortCode) : base()
        {
            _databaseShortCode = databaseShortCode;
            _datatableShortCode = datatableShortCode;
        }

        public async Task<DatatableResponse> GetEntireDatatable(string cursor = null)
        {
            string datatableUrl = $"{_databaseShortCode}/{_datatableShortCode}.json";

            if (cursor != null)
            {
                datatableUrl = String.Concat(datatableUrl, $"?qopts.cursor_id={cursor}");
            }

            using (HttpClient client = Client)
            {
                string requestResult = await QuandlRequest.GetStringAsync(client, datatableUrl);
                return JsonConvert.DeserializeObject<DatatableResponse>(requestResult);
            }
        }

        public async Task<DatatableResponse> GetFilteredDatatable(Dictionary<string, List<string>> rowSearchFilters, List<string> columnSearchFilters = null,  string cursor = null)
        {
            string datatableUrl = $"{_databaseShortCode}/{_datatableShortCode}.json";

            if (rowSearchFilters != null)
            {
                datatableUrl = ProcessRowFilters(rowSearchFilters, datatableUrl);
            }

            if (columnSearchFilters != null)
            {
                datatableUrl = ProcessColumnFilters(columnSearchFilters, datatableUrl);
            }

            if (cursor != null)
            {
                datatableUrl = String.Concat(datatableUrl, $"&qopts.cursor_id={cursor}");
            }

            using (HttpClient client = Client)
            {
                string requestResult = await QuandlRequest.GetStringAsync(client, datatableUrl);
                return JsonConvert.DeserializeObject<DatatableResponse>(requestResult);
            }
        }

        private static string ProcessRowFilters(Dictionary<string, List<string>> rowSearchFilters, string datatableUrl)
        {
            List<string> keys = rowSearchFilters.Keys.ToList();
            List<List<string>> values = rowSearchFilters.Values.ToList();
            for (int i = 0; i < rowSearchFilters.Count; i++)
            {
                if (i == 0)
                {
                    datatableUrl = String.Concat(datatableUrl, "?");
                }
                else
                {
                    datatableUrl = String.Concat(datatableUrl, "&");
                }

                datatableUrl = String.Concat(datatableUrl, keys[i]);
                datatableUrl = String.Concat(datatableUrl, "=");
                datatableUrl = String.Concat(datatableUrl, String.Join(",", values[i]));
            }
            return datatableUrl;
        }

        private static string ProcessColumnFilters(List<string> columnSearchFilters, string datatableUrl)
        {
            if (datatableUrl.Contains("?"))
            {
                datatableUrl = String.Concat(datatableUrl, "&"); 
            }
            else
            {
                datatableUrl = String.Concat(datatableUrl, "?");
            }

            string columnList = String.Join(",", columnSearchFilters);
            datatableUrl = String.Concat(datatableUrl, $"qopts.columns={columnList}");

            return datatableUrl;
        }
    }
}
