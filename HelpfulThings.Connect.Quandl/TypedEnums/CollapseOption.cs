namespace HelpfulThings.Connect.Quandl.TypedEnums
{
    public class CollapseOption
    {
        public static CollapseOption None = new CollapseOption("none");
        public static CollapseOption Daily = new CollapseOption("daily");
        public static CollapseOption Weekly = new CollapseOption("weekly");
        public static CollapseOption Monthly = new CollapseOption("monthly");
        public static CollapseOption Quarterly = new CollapseOption("quarterly");
        public static CollapseOption Annual = new CollapseOption("annual");

        public readonly string QueryStringValue;

        private CollapseOption(string queryStringValue)
        {
            QueryStringValue = queryStringValue;
        }
    }
}
