using System;

namespace HelpfulThings.Connect.Quandl.Statistics
{
    public class RequestStatistic
    {
        public int CurrentRateLimit { get; set; }

        public int RateLimitRemaining { get; set; }
        
        public DateTimeOffset Date { get; set; }

        public double RequestRunTime { get; set; }

        public Guid RequestId { get; set; }
    }
}
