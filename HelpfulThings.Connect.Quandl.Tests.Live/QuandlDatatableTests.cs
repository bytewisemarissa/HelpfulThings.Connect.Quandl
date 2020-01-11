using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HelpfulThings.Connect.Quandl.Models.DatatableModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelpfulThings.Connect.Quandl.Tests.Live
{
    [TestClass]
    public class QuandlDatatableTests
    {
        private QuandlDatatable _testDatatable;

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
        public void DatatableTestInitalizer()
        {
              _testDatatable = new QuandlDatatable("WIKI/PRICES");  
        }

        [TestMethod]
        public async Task Live_GetEntireDatatableTest()
        {
            DatatableResponse datatableTestResponse = await _testDatatable.GetEntireDatatable();

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

            DatatableResponse datatableTestResponse = await _testDatatable.GetFilteredDatatable(rowSearchFilters);

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

            DatatableResponse datatableTestResponse = await _testDatatable.GetFilteredDatatable(null, columnSearchFilters);

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

            DatatableResponse datatableTestResponse = await _testDatatable.GetFilteredDatatable(rowSearchFilters, columnSearchFilters);

            Assert.IsNotNull(datatableTestResponse);
            Assert.IsNotNull(datatableTestResponse.Datatable);
            Assert.IsNotNull(datatableTestResponse.CursorMetadata);

            Assert.IsTrue(datatableTestResponse.Datatable.Columns.Count > 0);
            Assert.IsTrue(datatableTestResponse.Datatable.Data.Count > 0);

            Assert.IsNull(datatableTestResponse.CursorMetadata.NextCursorId);
        }
    }
}
