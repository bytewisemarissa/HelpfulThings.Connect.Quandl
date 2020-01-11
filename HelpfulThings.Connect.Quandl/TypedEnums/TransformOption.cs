namespace HelpfulThings.Connect.Quandl.TypedEnums
{
    public class TransformOption
    {
        public static TransformOption None = new TransformOption("none");
        public static TransformOption Difference = new TransformOption("diff");
        public static TransformOption PercentageDifference = new TransformOption("rdiff");
        public static TransformOption PercentageIncrement = new TransformOption("rdiff_from");
        public static TransformOption Cumulative = new TransformOption("cumul");
        public static TransformOption Normalize = new TransformOption("normalize");

        public readonly string QueryStringValue;

        private TransformOption(string queryStringValue)
        {
            QueryStringValue = queryStringValue;
        }
    }
}
