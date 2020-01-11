namespace HelpfulThings.Connect.Quandl.TypedEnums
{
    public class ReturnFormat
    {
        public static readonly ReturnFormat JSON = new ReturnFormat("json");
        public static readonly ReturnFormat XML = new ReturnFormat("xml");
        public static readonly ReturnFormat CSV = new ReturnFormat("csv");

        public readonly string FormatExtension;

        private ReturnFormat(string formatExtension)
        {
            this.FormatExtension = formatExtension;
        }
    }
}
