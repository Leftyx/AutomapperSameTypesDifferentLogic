using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomapperSameTypesDifferentLogic.Infrastructure.Mapping.Profiles
{
    public class OrderToOrderMapperProfile001 : Profile
    {
        private readonly IMappingEngine AutomapperEngine = null;

        public OrderToOrderMapperProfile001(IMappingEngine mappingEngine)
        {
            this.AutomapperEngine = mappingEngine;
        }
        
        protected override void Configure()
        {
            this.CreateMap<Domain.Order, Domain.Order>()
                .WithProfile("Profile001")
                .ForMember(dest => dest.Code, opt => opt.MapFrom(source => System.Guid.Empty))
                .ForMember(dest => dest.OrderLines, opt => opt.Ignore())
                .AfterMap((source, destination) =>
                {
                    Console.WriteLine("OrderToOrderMapperProfile001 => Map For [Domain.Order] => [Profile001]");
                    foreach (var line in source.OrderLines)
                    {
                        destination.AddOrderLine(this.AutomapperEngine.Map<Domain.OrderLine, Domain.OrderLine>(line));
                    }

                });

            this.CreateMap<Domain.OrderLine, Domain.OrderLine>()
                .WithProfile("Profile001")
                .ForMember(dest => dest.Code, opt => opt.MapFrom(source => System.Guid.Empty))
                .ForMember(dest => dest.Order, opt => opt.Ignore())
                .AfterMap((source, destination) =>
                {
                    Console.WriteLine("OrderToOrderMapperProfile001 => Map For [Domain.OrderLine] => [Profile001]");
                    string itemCode = source.ItemCode;
                });

        }
    }
}
