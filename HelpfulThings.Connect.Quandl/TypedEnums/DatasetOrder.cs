namespace HelpfulThings.Connect.Quandl.TypedEnums
{
    public class DatasetOrder
    {
        public static DatasetOrder Ascending = new DatasetOrder("asc");
        public static DatasetOrder Descending = new DatasetOrder("desc");

        public readonly string QueryStringValue;

        private DatasetOrder(string queryStringValue)
        {
            QueryStringValue = queryStringValue;
        }
    }
}
