using Microsoft.Extensions.DependencyInjection;

namespace HelpfulThings.Connect.Quandl.IOCSupport
{
    public static class DotNetConnectQuandlConfigurationExtensions
    {
        public static void AddDotNetConnectQuandl(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IConnectorFactory, ConnectorFactory>();
        }
    }
}
