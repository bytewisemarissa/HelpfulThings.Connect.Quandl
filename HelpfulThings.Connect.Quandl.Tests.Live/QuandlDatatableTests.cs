using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelpfulThings.Connect.Quandl.Tests.Live
{
    [TestClass]
    public class QuandlDatatableTests : BaseLiveTest
    {
        [TestMethod]
        public async Task Live_GetEntireDatatableTest()
        {
            var datatableTestResponse = await TestApiClient.Datatable.GetEntireDatatable("WIKI/PRICES", null);

            Assert.IsNotNull(datatableTestResponse);
            Assert.IsNotNull(datatableTestResponse.Datatable);
            Assert.IsNotNull(datatableTestResponse.CursorMetadata);

            Assert.IsTrue(datatableTestResponse.Datatable.Columns.Count > 0);
            Assert.IsTrue(datatableTestResponse.Datatable.Data.Count > 0);

            Assert.IsNotNull(datatableTestResponse.CursorMetadata.NextCursorId);
        }

        [TestMethod]
        public async Task Live_GetFilteredDatatableRowTest()
        {
            Dictionary<string, List<string>> rowSearchFilters = new Dictionary<string, List<string>>();
            rowSearchFilters.Add("ticker", new List<string> { "AAMC", "A" });

            var datatableTestResponse = await TestApiClient.Datatable.GetFilteredDatatable(
                "WIKI/PRICES", 
                rowSearchFilters, 
                null, 
                null);

            Assert.IsNotNull(datatableTestResponse);
            Assert.IsNotNull(datatableTestResponse.Datatable);
            Assert.IsNotNull(datatableTestResponse.CursorMetadata);

            Assert.IsTrue(datatableTestResponse.Datatable.Columns.Count > 0);
            Assert.IsTrue(datatableTestResponse.Datatable.Data.Count > 0);

            Assert.IsNull(datatableTestResponse.CursorMetadata.NextCursorId);
        }

        [TestMethod]
        public async Task Live_GetFilteredDatatableColumnTest()
        {
            List<string> columnSearchFilters = new List<string> {"ticker", "date", "close"};

            var datatableTestResponse = await TestApiClient.Datatable.GetFilteredDatatable(
                "WIKI/PRICES",
                null, 
                columnSearchFilters,
                null);

            Assert.IsNotNull(datatableTestResponse);
            Assert.IsNotNull(datatableTestResponse.Datatable);
            Assert.IsNotNull(datatableTestResponse.CursorMetadata);

            Assert.IsTrue(datatableTestResponse.Datatable.Columns.Count > 0);
            Assert.IsTrue(datatableTestResponse.Datatable.Data.Count > 0);

            Assert.IsNotNull(datatableTestResponse.CursorMetadata.NextCursorId);
        }

        [TestMethod]
        public async Task Live_GetFilteredDatatableRowColumnTest()
        {
            Dictionary<string, List<string>> rowSearchFilters = new Dictionary<string, List<string>>();
            rowSearchFilters.Add("ticker", new List<string> { "AAMC", "A" });

            List<string> columnSearchFilters = new List<string> { "ticker", "date", "close" };

            var datatableTestResponse = await TestApiClient.Datatable.GetFilteredDatatable(
                "WIKI/PRICES",
                rowSearchFilters,
                columnSearchFilters,
                null);

            Assert.IsNotNull(datatableTestResponse);
            Assert.IsNotNull(datatableTestResponse.Datatable);
            Assert.IsNotNull(datatableTestResponse.CursorMetadata);

            Assert.IsTrue(datatableTestResponse.Datatable.Columns.Count > 0);
            Assert.IsTrue(datatableTestResponse.Datatable.Data.Count > 0);

            Assert.IsNull(datatableTestResponse.CursorMetadata.NextCursorId);
        }
    }
}
