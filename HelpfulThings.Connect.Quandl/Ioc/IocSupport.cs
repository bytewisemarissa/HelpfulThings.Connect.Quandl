using System;
using HelpfulThings.Connect.Quandl.Statistics;
using Microsoft.Extensions.DependencyInjection;

namespace HelpfulThings.Connect.Quandl.Ioc
{
    public static class IocSupport
    {
        public static void AddDotNetConnectQuandl(this IServiceCollection serviceCollection,
            Action<QuandlApiOptions> optionsAction = null)
        {
            var options = new QuandlApiOptions();
            optionsAction?.Invoke(options);

            serviceCollection.AddSingleton<QuandlApiOptions>(options);
            serviceCollection.AddTransient<IStatisticsCollector, StatisticsCollector>();
            serviceCollection.AddTransient<IRequestRouter,RequestRouter>();
            serviceCollection.AddTransient<IDatabase, Database>();
            serviceCollection.AddTransient<IDataset,Dataset>();
            serviceCollection.AddTransient<IDatatable,Datatable>();
            serviceCollection.AddTransient<IQuandlClient, QuandlClient>();
        }
    }
}
