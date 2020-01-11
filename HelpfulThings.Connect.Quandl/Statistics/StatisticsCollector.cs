using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace HelpfulThings.Connect.Quandl.Statistics
{
    public static class StatisticsCollector
    {
        public static bool CollectionEnabled { get; set; }
        public static int CollectionDepthRequests { get; set; }
        public static int CollectionSlopRequests { get; set; }

        private static readonly ConcurrentDictionary<Guid, RequestStatistic> RequestStatisticsCollection;
        private static readonly object RequestStatsLock;

        static StatisticsCollector()
        {
            CollectionEnabled = false;
            CollectionDepthRequests = 500;
            CollectionSlopRequests = 20;
            RequestStatisticsCollection = new ConcurrentDictionary<Guid, RequestStatistic>();
            RequestStatsLock = new object();
        }

        public static void ClearAllCollections()
        {
            RequestStatisticsCollection.Clear();
        }

        public static void CollectRequestStatistic(RequestStatistic stat)
        {
            if (!CollectionEnabled)
            {
                return;
            }

            if (CollectionDepthRequests > 0)
            {
                int maxCollectionDepth = CollectionDepthRequests + CollectionSlopRequests;
                lock (RequestStatsLock)
                {
                    if (RequestStatisticsCollection.Count > (maxCollectionDepth - 1))
                    {
                        int amountOfRecordsToRemove = RequestStatisticsCollection.Count -
                                                      (CollectionDepthRequests - CollectionSlopRequests);

                        IEnumerable<Guid> recordsToRemove =
                            RequestStatisticsCollection.OrderBy(dt => dt.Value.Date).Select(kv => kv.Key).Take(amountOfRecordsToRemove);

                        foreach (Guid record in recordsToRemove)
                        {
                            RequestStatistic throwAway;
                            RequestStatisticsCollection.TryRemove(record, out throwAway);
                        }
                    }

                    RequestStatisticsCollection.TryAdd(Guid.NewGuid(), stat);
                }
            }
            else
            {
                RequestStatisticsCollection.TryAdd(Guid.NewGuid(), stat);
            }
        }

        public static List<RequestStatistic> GetStatisticsCollection()
        {
            return RequestStatisticsCollection.Values.ToList();
        }
    }
}
