namespace HelpfulThings.Connect.Quandl.Ioc
{
    public class QuandlApiOptions
    {
        public string ApiKey { get; set; }
        public int StatisticsRollingAvgDataPoints { get; set; }
        public string UserAgent { get; set; }
        public string UserAgentVersion { get; set; }

        public QuandlApiOptions()
        {
            ApiKey = null;
            StatisticsRollingAvgDataPoints = 30;
            UserAgent = $"HelpfulThings-Connect-Quandl";
            UserAgentVersion = GetType().Assembly.GetName().Version.ToString();
        }
    }
}