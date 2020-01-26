namespace HelpfulThings.Connect.Quandl
{
    public static class Endpoints
    {
        public const string QuandlApiBase = "https://www.quandl.com/api/v3";
        
        public static class Database
        {
            public const string List = "/databases.json";
            public const string CsvFile = "/databases/{0}/data";
            public const string DatasetListCsvFile = "/databases/{0}/codes.csv";
            public const string Metadata = "/databases/{0}.json";
        }
        
        public static class Dataset
        {
            public const string DataOnly = "/datasets/{0}/{1}/data.json";
            public const string MetadataOnly = "/datasets/{0}/{1}/metadata.json";
            public const string Complete = "/datasets/{0}/{1}.json";
            public const string DatasetSearch = "/datasets.json";
        }
        
        public static class Datatable
        {
            public const string GetDatatable = "/datatables/{0}/{1}.json";
        }
    }
}