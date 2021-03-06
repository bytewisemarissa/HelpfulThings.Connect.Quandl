using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelpfulThings.Connect.Quandl.Ioc;

namespace HelpfulThings.Connect.Quandl.Tests.Live
{
    public class BaseLiveTest
    {
        private static IServiceProvider _serviceProvider;
        protected IQuandlClient TestApiClient;
        
        [TestInitialize]
        public void TestInitialize()
        {
            InitIoc();
            TestApiClient = (IQuandlClient)_serviceProvider.GetService(typeof(IQuandlClient));
        }

        private static void InitIoc()
        {
            string apiKey;

            try
            {
                apiKey = File.ReadAllText("Secrets/apikey.secrets");
            }
            catch (Exception ex)
            {
                throw new Exception("Problems loading the ApiKey.Private file. See the read me for help.", ex);
            }
            
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDotNetConnectQuandl(opts =>
            {
                opts.ApiKey = apiKey;
                opts.StatisticsRollingAvgDataPoints = 30;
                opts.UserAgent = "HelpfulThings.Connect.Quandl";
                opts.UserAgentVersion = "";
            });

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}