using System;
using HelpfulThings.Connect.Quandl.Ioc;

namespace HelpfulThings.Connect.Quandl.Statistics
{
    public interface IStatisticsCollector
    {
        void CollectRequestStatistics(
            int currentRate, 
            int rateLimitRemaining, 
            double requestTime,
            DateTimeOffset lastRequestTime);
    }

    public class StatisticsCollector : IStatisticsCollector
    {
        private readonly QuandlApiOptions _options;
        private int _totalDataPoints;
        private readonly object _statsLock;
        
        public readonly ApiStatistics Statistics;
        
        public StatisticsCollector(QuandlApiOptions options)
        {
            _options = options;
            _totalDataPoints = 0;
            _statsLock = new object();
            Statistics = new ApiStatistics();
        }

        public void CollectRequestStatistics(
            int currentRate, 
            int rateLimitRemaining, 
            double requestTime,
            DateTimeOffset lastRequestTime)
        {
            lock (_statsLock)
            {
                if (lastRequestTime < Statistics.LastRequestTime)
                {
                    return;
                }

                Statistics.LastRequestTime = lastRequestTime;
                Statistics.CurrentLimit = currentRate;
                Statistics.RateLimitRemaining = rateLimitRemaining;

                if (_totalDataPoints != _options.StatisticsRollingAvgDataPoints + 1)
                {
                    _totalDataPoints++;
                }

                Statistics.AverageRequestRunTime = (requestTime +
                                                    ((_totalDataPoints - 1) * Statistics.AverageRequestRunTime)) /
                                                   _totalDataPoints;
            }
        }
    }
}
