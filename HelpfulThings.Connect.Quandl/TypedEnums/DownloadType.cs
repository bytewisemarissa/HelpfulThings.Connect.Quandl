namespace HelpfulThings.Connect.Quandl.TypedEnums
{
    public class DownloadType
    {
        public static readonly DownloadType Complete = new DownloadType("complete");
        public static readonly DownloadType Partial = new DownloadType("partial");
        
        public readonly string UrlValue;

        private DownloadType(string urlValue)
        {
            this.UrlValue = urlValue;
        }
    }
}
