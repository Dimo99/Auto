using DataColector.CarsBg;
using Microsoft.Extensions.DependencyInjection;
using Persistance;
using System;

namespace DataColector
{
    public class DataCollectorFactory
    {
        private readonly IServiceProvider serviceProvider;

        public DataCollectorFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IDataCollector GetDataCollectorByType(SourceEnum sourceEnum)
        {
            switch (sourceEnum)
            {
                case SourceEnum.CarsBg:
                    return serviceProvider.GetService<CarsBgDataCollector>();
            }

            throw new ArgumentException("Not supported collector type");
        }
    }
}
