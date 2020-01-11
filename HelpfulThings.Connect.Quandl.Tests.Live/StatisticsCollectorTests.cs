using System;
using System.Collections.Generic;
using System.Linq;
using HelpfulThings.Connect.Quandl.Statistics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelpfulThings.Connect.Quandl.Tests.Live
{
    [TestClass]
    public class StatisticsCollectorTests
    {
        [TestInitialize]
        public void InitalizeStatCollectionTest()
        {
            StatisticsCollector.ClearAllCollections();
        }

        [TestMethod]
        public void Live_TestRequestStatCollection()
        {
            RequestStatistic testStat = new RequestStatistic()
            {
                CurrentRateLimit = 10,
                RateLimitRemaining = 10,
                Date = DateTimeOffset.UtcNow,
                RequestRunTime = 0.001,
                RequestId = Guid.NewGuid()
            };

            StatisticsCollector.CollectionEnabled = false;
            StatisticsCollector.CollectRequestStatistic(testStat);

            List<RequestStatistic> testResultList = StatisticsCollector.GetStatisticsCollection();

            Assert.IsNotNull(testResultList);
            Assert.AreEqual(0, testResultList.Count);

            StatisticsCollector.CollectionEnabled = true;
            StatisticsCollector.CollectRequestStatistic(testStat);

            testResultList = StatisticsCollector.GetStatisticsCollection();

            Assert.IsNotNull(testResultList);
            Assert.AreEqual(1, testResultList.Count);
            RequestStatistic testStatRecall = testResultList.First();
            Assert.AreEqual(testStat.CurrentRateLimit, testStatRecall.CurrentRateLimit);
            Assert.AreEqual(testStat.RateLimitRemaining, testStatRecall.RateLimitRemaining);
            Assert.AreEqual(testStat.Date, testStatRecall.Date);
            Assert.AreEqual(testStat.RequestRunTime, testStatRecall.RequestRunTime);
            Assert.AreEqual(testStat.RequestId, testStatRecall.RequestId);
        }

        [TestMethod]
        public void Live_TestStatCollectionRolloff()
        {
            RequestStatistic testStat = new RequestStatistic()
            {
                CurrentRateLimit = 10,
                RateLimitRemaining = 10,
                Date = DateTimeOffset.UtcNow,
                RequestRunTime = 0.001,
                RequestId = Guid.NewGuid()
            };

            StatisticsCollector.CollectionEnabled = true;
            StatisticsCollector.CollectionDepthRequests = 6;
            StatisticsCollector.CollectionSlopRequests = 2;

            for (int i = 0; i < 6; i++)
            {
                StatisticsCollector.CollectRequestStatistic(testStat);
                testStat.Date = DateTimeOffset.UtcNow;
            }

            List<RequestStatistic> testResultList = StatisticsCollector.GetStatisticsCollection();

            Assert.IsNotNull(testResultList);
            Assert.AreEqual(6, testResultList.Count);

            for (int i = 0; i < 2; i++)
            {
                StatisticsCollector.CollectRequestStatistic(testStat);
                testStat.Date = DateTimeOffset.UtcNow;
            }

            testResultList = StatisticsCollector.GetStatisticsCollection();

            Assert.IsNotNull(testResultList);
            Assert.AreEqual(8, testResultList.Count);

            StatisticsCollector.CollectRequestStatistic(testStat);

            testResultList = StatisticsCollector.GetStatisticsCollection();

            Assert.IsNotNull(testResultList);
            Assert.AreEqual(5, testResultList.Count);
        }
    }
}
