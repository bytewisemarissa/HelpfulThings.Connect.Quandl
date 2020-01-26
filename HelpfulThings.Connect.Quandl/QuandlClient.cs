namespace HelpfulThings.Connect.Quandl
{
    public interface IQuandlClient
    {
        IDatabase Database { get; }
        IDataset Dataset { get; }
        IDatatable Datatable { get; }
    }

    public class QuandlClient : IQuandlClient
    {
        public IDatabase Database { get; }
        public IDataset Dataset { get; }
        public IDatatable Datatable { get; }
        
        public QuandlClient(
            IDatabase database, 
            IDataset dataset, 
            IDatatable datatable)
        {
            Database = database;
            Dataset = dataset;
            Datatable = datatable;
        }
    }
}