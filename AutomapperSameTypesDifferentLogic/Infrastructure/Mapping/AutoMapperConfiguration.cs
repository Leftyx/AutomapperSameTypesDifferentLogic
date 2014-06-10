using AutoMapper;
using AutomapperSameTypesDifferentLogic.Infrastructure.Mapping.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomapperSameTypesDifferentLogic.Infrastructure.Mapping
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                Mapper.AddProfile(new OrderToOrderMapperProfile001(Mapper.Engine));
            });

            Mapper.AllowNullDestinationValues = true;

            Mapper.AssertConfigurationIsValid();
        }
    }
}
