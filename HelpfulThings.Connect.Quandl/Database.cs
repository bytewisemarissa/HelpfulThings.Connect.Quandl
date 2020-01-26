using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using HelpfulThings.Connect.Quandl.Models.Database;
using HelpfulThings.Connect.Quandl.TypedEnums;

namespace HelpfulThings.Connect.Quandl
{
    public interface IDatabase
    {
        Task<ListingResult> GetDatabaseList(int perPage = 100, int page = 1);
        Task<SearchResults> GetSearchResultsList(string[] searchParams, int perPage = 100, int page = 1);
        Task<Dictionary<string, string>> GetEntireDatabaseCsvFile(string databaseShortCode, DownloadType downloadType);
        Task<Metadata> GetDatabaseMetadata(string databaseShortCode);
    }

    public class Database : IDatabase
    {
        private readonly IRequestRouter _requestRouter;

        public Database(IRequestRouter requestRouter)
        {
            _requestRouter = requestRouter;
        }

        public async Task<ListingResult> GetDatabaseList(int perPage = 100, int page = 1)
        {
            var parameters = new Dictionary<string, string>
            {
                {"per_page", perPage.ToString()},
                {"page", page.ToString()}
            };
            
            return await _requestRouter.MakeRequest<ListingResult>(
                Endpoints.Database.List,
                parameters);
        }

        public async Task<SearchResults> GetSearchResultsList(string[] searchParams, int perPage = 100, int page = 1)
        {
            var parameters = new Dictionary<string, string>
            {
                {"query", String.Join("+", searchParams) },
                {"per_page", perPage.ToString()},
                {"page", page.ToString()}
            };
            
            return await _requestRouter.MakeRequest<SearchResults>(
                Endpoints.Database.List,
                parameters);
        }

        public async Task<Dictionary<string, string>> GetEntireDatabaseCsvFile(string databaseShortCode, DownloadType downloadType)
        {
            var parameters = new Dictionary<string, string>
            {
                {"download_type", downloadType.UrlValue }
            };
            
            var pathReplacements = new List<string>()
            {
                databaseShortCode
            };
            
            var returnValue = new Dictionary<string, string>();

            await using (var zippedReturnStream = await _requestRouter.MakeRequestStream(
                Endpoints.Database.CsvFile,
                parameters,
                pathReplacements))
            {
                using var archive = new ZipArchive(zippedReturnStream);
                foreach (var entry in archive.Entries)
                {
                    using var reader = new StreamReader(entry.Open());
                    returnValue.Add(entry.Name, reader.ReadToEnd());
                }
            }

            return returnValue;
        }

        public async Task<Metadata> GetDatabaseMetadata(string databaseShortCode)
        {
            var pathReplacements = new List<string>()
            {
                databaseShortCode
            };

            var requestResult = await _requestRouter.MakeRequest<MetadataResponse>(
                Endpoints.Database.Metadata,
                null, 
                pathReplacements);
                
            return requestResult.Database;
        }
    }
}
