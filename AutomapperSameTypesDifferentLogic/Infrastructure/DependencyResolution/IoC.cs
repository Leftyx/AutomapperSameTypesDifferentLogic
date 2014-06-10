using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomapperSameTypesDifferentLogic.Infrastructure.DependencyResolution
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(init =>
            {
                init.Scan(scan =>
                {
                    scan.WithDefaultConventions();
                });
                init.AddRegistry<AutomapperRegistry>();
            });

#if DEBUG
            //            ObjectFactory.AssertConfigurationIsValid();
            var what = ObjectFactory.Container.WhatDoIHave();
#endif

            return (ObjectFactory.Container);
        }
    }
}
