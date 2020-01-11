using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HelpfulThings.Connect.Quandl.Models.DatabaseModels;
using HelpfulThings.Connect.Quandl.TypedEnums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelpfulThings.Connect.Quandl.Tests.Live
{
    [TestClass]
    public class QuandlDatabaseTests
    {
        private QuandlDatabase _testDatabase = null;

        [ClassInitialize]
        public static void DatabaseUnitTestsInialization(TestContext context)
        {
            using (FileStream secretsFile = File.Open("Secrets/apikey.secrets", FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(secretsFile))
                {
                    QuandlRequest.ApiKey = reader.ReadToEnd();
                }
            }
        }

        [TestInitialize]
        public void InitalizeDatabaseTest()
        {
            _testDatabase = new QuandlDatabase("WIKI");
        }

        [TestMethod]
        public async Task Live_GetDatabaseTest()
        {
            Dictionary<string, Stream> testReturnValue = await _testDatabase.GetEntireDatabaseCsvFile(DownloadType.Partial);

            Assert.AreEqual(1, testReturnValue.Count);
            Assert.IsNotNull(testReturnValue.Values.First());

            //testReturnValue = await 
            //_testDatabase.GetEntireDatabaseCSVFile(DownloadType.Complete);

            //Assert.AreEqual(1, testReturnValue.Count);
            //Assert.IsNotNull(testReturnValue.Values.First());
        }

        [TestMethod]
        public async Task Live_GetDatabaseListTest()
        {
            DatabaseListingResult listingTestResult = await QuandlDatabase.GetDatabaseList(1, 1);

            Assert.IsNotNull(listingTestResult);
            Assert.IsNotNull(listingTestResult.Databases);
            Assert.IsNotNull(listingTestResult.PagingMetadata);

            Assert.AreEqual(1, listingTestResult.Databases.Count);
            Assert.AreEqual(1, listingTestResult.PagingMetadata.CurrentPageIndex);
        }
        
        [TestMethod]
        public async Task Live_DatabaseSearchTest()
        {
            DatabaseSearchResults searchTestResult = await QuandlDatabase.GetSearchResultsList(new string[] {"wiki"}, 10, 1);

            Assert.IsNotNull(searchTestResult);
            Assert.IsNotNull(searchTestResult.Databases);
            Assert.IsNotNull(searchTestResult.Metadata);

            Assert.IsTrue(searchTestResult.Databases.Count > 0);
            Assert.AreEqual(1, searchTestResult.Metadata.CurrentPageIndex);
        }

        [TestMethod]
        public async Task Live_GetDatabaseMetaDataTest()
        {
            DatabaseMetadata testResult = await _testDatabase.GetDatabaseMetadata();

            Assert.IsNotNull(testResult);
            Assert.AreEqual(4922, testResult.Id);
            Assert.AreEqual("Wiki EOD Stock Prices", testResult.Name);
            Assert.AreEqual("WIKI", testResult.DatabaseCode);
            Assert.AreEqual("End of day stock prices, dividends and splits for 3,000 US companies, curated by the Quandl community and released into the public domain.", testResult.Description);
            Assert.IsTrue(testResult.DatasetsCount > 0);
            Assert.IsNotNull(testResult.Downloads);
            Assert.AreEqual(false, testResult.Premium);
            Assert.AreEqual("https://quandl-production-data-upload.s3.amazonaws.com/uploads/source/profile_image/4922/thumb_thumb_quandl-open-data-logo.jpg", testResult.ImageUrl);
            Assert.AreEqual(false, testResult.Favorite);
        }

        [TestMethod]
        public async Task Live_GetDatabaseConentsTest()
        {
            Dictionary<string, string[][]> testReturnValue = await _testDatabase.GetDatabaseDatasetListCsv();

            Assert.AreEqual(1, testReturnValue.Count);
            string[][] listing = testReturnValue.Values.First();
            Assert.IsTrue(listing.Length > 0);
            Assert.IsTrue(listing[0].Length > 0);
        }
    }
}
