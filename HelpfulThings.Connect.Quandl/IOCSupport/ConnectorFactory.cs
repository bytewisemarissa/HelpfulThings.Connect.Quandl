namespace HelpfulThings.Connect.Quandl.IOCSupport
{
    public interface IConnectorFactory
    {
        QuandlDatabase CreateDatabase(string databaseShortCode);
        QuandlDataset CreateDataset(string combinedShortCode);
        QuandlDataset CreateDataset(string databaseShortCode, string datasetShortCode);
        QuandlDatatable CreateDatatable(string combinedShortCode);
        QuandlDatatable CreateDatatable(string databaseShortCode, string datasetShortCode);
    }

    public class ConnectorFactory : IConnectorFactory
    {
        public QuandlDatabase CreateDatabase(string databaseShortCode)
        {
            return new QuandlDatabase(databaseShortCode);
        }

        public QuandlDataset CreateDataset(string combinedShortCode)
        {
            return new QuandlDataset(combinedShortCode);
        }

        public QuandlDataset CreateDataset(string databaseShortCode, string datasetShortCode)
        {
            return new QuandlDataset(databaseShortCode, datasetShortCode);
        }

        public QuandlDatatable CreateDatatable(string combinedShortCode)
        {
            return new QuandlDatatable(combinedShortCode);
        }

        public QuandlDatatable CreateDatatable(string databaseShortCode, string datasetShortCode)
        {
            return new QuandlDatatable(databaseShortCode, datasetShortCode);
        }
    }
}
