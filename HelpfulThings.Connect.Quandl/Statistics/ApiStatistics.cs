using System;

namespace HelpfulThings.Connect.Quandl.Statistics
{
    public class ApiStatistics
    {
        public DateTimeOffset LastRequestTime { get; set; }
        public int CurrentLimit { get; set; }
        
        public int RateLimitRemaining { get; set; }

        public double AverageRequestRunTime { get; set; }
    }
}
