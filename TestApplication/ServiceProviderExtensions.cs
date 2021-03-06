﻿using DataColector;
using Persistance;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TestApplication
{
    public static class ServiceProviderExtensions
    {
        public static IDataCollector GetDataCollector(this IServiceProvider serviceProvider, SourceEnum sourceEnum)
        {
            DataCollectorFactory dataCollectorFactory = serviceProvider.GetService<DataCollectorFactory>();
            return dataCollectorFactory.GetDataCollectorByType(sourceEnum);
        }
    }
}
