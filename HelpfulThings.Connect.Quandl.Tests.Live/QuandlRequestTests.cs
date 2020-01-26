using System.IO;
using System.Threading.Tasks;
using HelpfulThings.Connect.Quandl.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelpfulThings.Connect.Quandl.Tests.Live
{
    [TestClass]
    public class QuandlRequestTests
    {
        [ClassInitialize]
        public static void DatabaseUnitTestsInialization(TestContext context)
        {
            using (FileStream secretsFile = File.Open("Secrets/apikey.secrets", FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(secretsFile))
                {
                    //QuandlRequest.ApiKey = reader.ReadToEnd();
                }
            }
        }

        [TestMethod]
        public async Task Live_QuandlRequestErrorHandlingTest()
        {
//            Database testDatabase = new Database("XXXX");
//            try
//            {
//                await testDatabase.GetDatabaseMetadata();
//            }
//            catch (QuandlErrorException exception)
//            {
//                Assert.AreEqual("QECx02", exception.Code);
//                Assert.AreEqual("You have submitted an incorrect Quandl code. Please check your Quandl codes and try again.", exception.Message);
//            }
        }
    }
}
