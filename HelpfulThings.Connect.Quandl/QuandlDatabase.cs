using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;
using CsvHelper;
using HelpfulThings.Connect.Quandl.Models.DatabaseModels;
using HelpfulThings.Connect.Quandl.TypedEnums;
using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl
{
    public class QuandlDatabase : QuandlRequest
    {
        private HttpClient Client => new HttpClient()
        {
            BaseAddress = new Uri("https://www.quandl.com/api/v3/databases/")
        };

        private readonly string _databaseShortCode;

        public QuandlDatabase(string databaseShortCode) : base()
        {
            _databaseShortCode = databaseShortCode;
        }

        public static async Task<DatabaseListingResult> GetDatabaseList(int perPage = 100, int page = 1)
        {
            string databaseListingUrl = $"databases.json?per_page={perPage}&page={page}";

            using (HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri($"https://www.quandl.com/api/v3/")
            })
            {
                string requestResult = await QuandlRequest.GetStringAsync(client, databaseListingUrl);
                return JsonConvert.DeserializeObject<DatabaseListingResult>(requestResult);
            }
        }

        public static async Task<DatabaseSearchResults> GetSearchResultsList(string[] searchParms, int perPage = 100, int page = 1)
        {
            string searchParmsString = String.Join("+", searchParms);
            string databaseSearchUrl = $"databases.json?query={searchParmsString}&per_page={perPage}&page={page}";

            using (HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri($"https://www.quandl.com/api/v3/")
            })
            {
                string requestResult = await QuandlRequest.GetStringAsync(client, databaseSearchUrl);
                return JsonConvert.DeserializeObject<DatabaseSearchResults>(requestResult);
            }
        }

        public async Task<Dictionary<string, Stream>> GetEntireDatabaseCsvFile(DownloadType downloadType)
        {
            string databaseUrl = $"{_databaseShortCode}/data?download_type={downloadType.UrlValue}";

            Dictionary<string, Stream> returnValue = new Dictionary<string, Stream>();
            using (HttpClient client = Client)
            {
                Stream zippedReturnStream = await QuandlRequest.GetStreamAsync(client, databaseUrl);
                using (ZipArchive archive = new ZipArchive(zippedReturnStream))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        returnValue.Add(entry.Name, entry.Open());
                    }
                }
            }
            
            return returnValue;
        }

        public async Task<Dictionary<string, string[][]>> GetDatabaseDatasetListCsv()
        {
            string databaseContentsUrl = $"{_databaseShortCode}/data.csv";

            Dictionary<string, string[][]> returnValue = new Dictionary<string, string[][]>();
            using (HttpClient client = Client)
            {
                Stream zippedReturnStream = await QuandlRequest.GetStreamAsync(client, databaseContentsUrl);
                using (ZipArchive archive = new ZipArchive(zippedReturnStream))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        using (TextReader reader = new StreamReader(entry.Open()))
                        {
                            CsvParser parser = new CsvParser(reader);
                            List<string[]> contents = new List<string[]>();
                            while(true)
                            {
                                string[] rowValues = parser.Read();
                                if (rowValues == null)
                                    break;
                                contents.Add(rowValues);
                            }
                            returnValue.Add(entry.Name, contents.ToArray());
                        }
                            
                    }
                }
            }

            return returnValue;
        }

        public async Task<DatabaseMetadata> GetDatabaseMetadata()
        {
            string metadataUrl = $"{_databaseShortCode}.json";

            using (HttpClient client = Client)
            {
                string requestResult = await QuandlRequest.GetStringAsync(client, metadataUrl);
                MetadataCallResult returnValue = JsonConvert.DeserializeObject<MetadataCallResult>(requestResult);
                return returnValue.Database;
            }
        }
    }
}
