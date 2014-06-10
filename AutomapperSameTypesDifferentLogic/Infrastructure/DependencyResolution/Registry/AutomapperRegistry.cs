using AutoMapper;
using AutoMapper.Mappers;
using AutomapperSameTypesDifferentLogic.Infrastructure.Mapping;
using AutomapperSameTypesDifferentLogic.Infrastructure.Mapping.Profiles;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomapperSameTypesDifferentLogic.Infrastructure.DependencyResolution
{
    public class AutomapperRegistry: Registry
    {
        public AutomapperRegistry()
        {
            this.For<IMappingEngine>()
                .LifecycleIs<SingletonLifecycle>()
                .Use(StandardMappingEngine())
                .Named("StandardMappingEngine");

            this.For<IMappingEngine>()
                .LifecycleIs<SingletonLifecycle>()
                .Use(CustomMappingEngine())
                .Named("CustomMappingEngine");
        }

        private IMappingEngine StandardMappingEngine()
        {
            Mapper.Initialize(x =>
            {
                Mapper.AddProfile(new OrderToOrderMapperProfile001(Mapper.Engine));
            });

            Mapper.AllowNullDestinationValues = true;

            Mapper.AssertConfigurationIsValid();

            return (Mapper.Engine);
        }

        private IMappingEngine StandardMappingEngineConfigured()
        {
            ConfigurationStore store = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);
            MappingEngine engine = new MappingEngine(store);
            store.AddProfile(new OrderToOrderMapperProfile001(engine));
            store.AllowNullDestinationValues = true;
            store.AssertConfigurationIsValid();
            return (engine);
        }

        private IMappingEngine CustomMappingEngine()
        {
            ConfigurationStore store = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);
            MappingEngine engine = new MappingEngine(store);
            store.AddProfile(new OrderToOrderMapperProfile002(engine));
            store.AllowNullDestinationValues = true;
            store.AssertConfigurationIsValid();
            return (engine);
        }
    }

    public class StandardEngine
    { 
    
    }
}
