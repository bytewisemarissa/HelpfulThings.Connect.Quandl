using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HelpfulThings.Connect.Quandl.Models.Dataset;
using HelpfulThings.Connect.Quandl.TypedEnums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelpfulThings.Connect.Quandl.Tests.Live
{
    [TestClass]
    public class DatasetLiveTests : BaseLiveTest
    {
        [TestMethod]
        public async Task Live_DatesetGetDataOnlyTest()
        {
            var testResponse = await TestApiClient.Dataset.GetDatasetDataOnly("WIKI/FB");

            Assert.IsNotNull(testResponse);
            Assert.IsNull(testResponse.Collapse);
            Assert.IsNull(testResponse.ColumnIndex);
            Assert.AreEqual(13, testResponse.ColumnNames.Count);
            Assert.IsNotNull(testResponse.EndDate);
            Assert.AreEqual("daily", testResponse.Frequency);
            Assert.IsNull(testResponse.Limit);
            Assert.IsNull(testResponse.Order);
            Assert.IsNotNull(testResponse.StartDate);
            Assert.AreEqual(true, testResponse.TableData.Count > 0);
            Assert.IsNull(testResponse.Transform);
        }

        [TestMethod]
        public async Task Live_DatasetGetMetadataOnlyTest()
        {
            var testResponse = await TestApiClient.Dataset.GetDatasetMetadataOnly("WIKI/FB");

            Assert.IsNotNull(testResponse);
            Assert.AreEqual(9775687, testResponse.DatasetId);
            Assert.AreEqual("FB", testResponse.DatasetCode);
            Assert.AreEqual("WIKI", testResponse.DatabaseCode);
            Assert.AreEqual("Facebook Inc. (FB) Prices, Dividends, Splits and Trading Volume", testResponse.Name);
            Assert.AreEqual("End of day open, high, low, close and volume, dividends and splits, and split/dividend adjusted open, high, low close and volume for Facebook, Inc. (FB). Ex-Dividend is non-zero on ex-dividend dates. Split Ratio is 1 on non-split dates. Adjusted prices are calculated per CRSP (www.crsp.com/products/documentation/crsp-calculations)\n\nThis data is in the public domain. You may copy, distribute, disseminate or include the data in other products for commercial and/or noncommercial purposes.\n\nThis data is part of Quandl's Wiki initiative to get financial data permanently into the public domain. Quandl relies on users like you to flag errors and provide data where data is wrong or missing. Get involved: connect@quandl.com\n", testResponse.Description);
            Assert.IsNotNull(testResponse.RefreshedAt);
            Assert.IsNotNull(testResponse.NewestDateAvailable);
            Assert.IsNotNull(testResponse.OldestDateAvailable);
            Assert.AreEqual(13, testResponse.ColumnNames.Count);
            Assert.AreEqual("daily", testResponse.Frequency);
            Assert.AreEqual("Time Series", testResponse.Type);
            Assert.AreEqual(false, testResponse.Premium);
            Assert.AreEqual(4922, testResponse.DatabaseId);
        }

        [TestMethod]
        public async Task Live_DatasetGetCompleteTest()
        {
            var testResponse = await TestApiClient.Dataset.GetDatasetComplete("WIKI/FB");

            Assert.IsNotNull(testResponse);
            Assert.IsNull(testResponse.Collapse);
            Assert.IsNull(testResponse.ColumnIndex);
            Assert.AreEqual(13, testResponse.ColumnNames.Count);
            Assert.IsNotNull(testResponse.EndDate);
            Assert.AreEqual("daily", testResponse.Frequency);
            Assert.IsNull(testResponse.Limit);
            Assert.IsNull(testResponse.Order);
            Assert.IsNotNull(testResponse.StartDate);
            Assert.AreEqual(true, testResponse.TableData.Count > 0);
            Assert.IsNull(testResponse.Transform);
            Assert.IsNotNull(testResponse);
            Assert.AreEqual(9775687, testResponse.DatasetId);
            Assert.AreEqual("FB", testResponse.DatasetCode);
            Assert.AreEqual("WIKI", testResponse.DatabaseCode);
            Assert.AreEqual("Facebook Inc. (FB) Prices, Dividends, Splits and Trading Volume", testResponse.Name);
            Assert.AreEqual("End of day open, high, low, close and volume, dividends and splits, and split/dividend adjusted open, high, low close and volume for Facebook, Inc. (FB). Ex-Dividend is non-zero on ex-dividend dates. Split Ratio is 1 on non-split dates. Adjusted prices are calculated per CRSP (www.crsp.com/products/documentation/crsp-calculations)\n\nThis data is in the public domain. You may copy, distribute, disseminate or include the data in other products for commercial and/or noncommercial purposes.\n\nThis data is part of Quandl's Wiki initiative to get financial data permanently into the public domain. Quandl relies on users like you to flag errors and provide data where data is wrong or missing. Get involved: connect@quandl.com\n", testResponse.Description);
            Assert.IsNotNull(testResponse.RefreshedAt);
            Assert.IsNotNull(testResponse.NewestDateAvailable);
            Assert.IsNotNull(testResponse.OldestDateAvailable);
            Assert.AreEqual("Time Series", testResponse.Type);
            Assert.AreEqual(false, testResponse.Premium);
            Assert.AreEqual(4922, testResponse.DatabaseId);
        }

        [TestMethod]
        public async Task Live_DatasetGetCompleteFilterTest()
        {
            FilterOptions options = new FilterOptions()
            {
                Limit = 3,
                Collapse = CollapseOption.None,
                ColumnIndex = 2,
                StartDate = DateTime.UtcNow.AddDays(-5),
                EndDate = DateTime.UtcNow,
                Order = DatasetOrder.Descending,
                Transform = TransformOption.None
            };

            var testResponse = await TestApiClient.Dataset.GetDatasetComplete("WIKI/FB", options);

            Assert.IsNotNull(testResponse);

            Assert.IsNotNull(testResponse);
            Assert.IsNull(testResponse.Collapse);
            Assert.AreEqual(2, testResponse.ColumnIndex);
            Assert.AreEqual(2, testResponse.ColumnNames.Count);
            Assert.IsNotNull(testResponse.EndDate);
            Assert.AreEqual("daily", testResponse.Frequency);
            Assert.AreEqual(3, testResponse.Limit);
            Assert.AreEqual("desc", testResponse.Order);
            Assert.IsNotNull(testResponse.StartDate);
            Assert.AreEqual(0, testResponse.TableData.Count);
            Assert.IsNull(testResponse.Transform);
            Assert.AreEqual(9775687, testResponse.DatasetId);
            Assert.AreEqual("FB", testResponse.DatasetCode);
            Assert.AreEqual("WIKI", testResponse.DatabaseCode);
            Assert.AreEqual("Facebook Inc. (FB) Prices, Dividends, Splits and Trading Volume", testResponse.Name);
            Assert.AreEqual("End of day open, high, low, close and volume, dividends and splits, and split/dividend adjusted open, high, low close and volume for Facebook, Inc. (FB). Ex-Dividend is non-zero on ex-dividend dates. Split Ratio is 1 on non-split dates. Adjusted prices are calculated per CRSP (www.crsp.com/products/documentation/crsp-calculations)\n\nThis data is in the public domain. You may copy, distribute, disseminate or include the data in other products for commercial and/or noncommercial purposes.\n\nThis data is part of Quandl's Wiki initiative to get financial data permanently into the public domain. Quandl relies on users like you to flag errors and provide data where data is wrong or missing. Get involved: connect@quandl.com\n", testResponse.Description);
            Assert.IsNotNull(testResponse.RefreshedAt);
            Assert.IsNotNull(testResponse.NewestDateAvailable);
            Assert.IsNotNull(testResponse.OldestDateAvailable);
            Assert.AreEqual("Time Series", testResponse.Type);
            Assert.AreEqual(false, testResponse.Premium);
            Assert.AreEqual(4922, testResponse.DatabaseId);
        }

        [TestMethod]
        public async Task Live_DatasetGetSearchResultsTest()
        {
            var testResponse = await TestApiClient.Dataset.GetDatasetSearchResult(
                new List<string> {"us", "currency"},
                "BOE");
            

            Assert.IsNotNull(testResponse);

            Assert.AreEqual(100, testResponse.Datasets.Count);

            Assert.AreEqual(1, testResponse.Metadata.CurrentFirstItem);
            Assert.AreEqual(100, testResponse.Metadata.CurrentLastItem);
            Assert.AreEqual(1, testResponse.Metadata.CurrentPageIndex);
            Assert.AreEqual(100, testResponse.Metadata.ItemsPerPage);
            Assert.IsNotNull(testResponse.Metadata.NextPageIndex);
            Assert.AreEqual(2, testResponse.Metadata.NextPageIndex.Value);
            Assert.IsNull(testResponse.Metadata.PreviousPageIndex);
            Assert.AreEqual("us currency", testResponse.Metadata.Query);
            Assert.AreEqual(true, testResponse.Metadata.TotalItemCount >= 100);
            Assert.AreEqual(true, testResponse.Metadata.TotalPages >= 1);
        }
    }
}
