using System.IO;
using System.Threading.Tasks;
using HelpfulThings.Connect.Quandl.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelpfulThings.Connect.Quandl.Tests.Live
{
    [TestClass]
    public class ErrorHandlingTests : BaseLiveTest
    {
        [TestMethod]
        public async Task Live_QuandlRequestErrorHandlingTest()
        {
            try
            {
                await TestApiClient.Database.GetDatabaseMetadata("XXXX");
            }
            catch (QuandlErrorException exception)
            {
                Assert.AreEqual("QECx02", exception.Code);
                Assert.AreEqual("You have submitted an incorrect Quandl code. Please check your Quandl codes and try again.", exception.Message);
            }
        }
    }
}
