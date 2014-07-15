using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomapperSameTypesDifferentLogic.Infrastructure.Mapping.Profiles
{
    public class OrderToOrderMapperProfile002 : Profile
    {
        private readonly IMappingEngine AutomapperEngine = null;

        public OrderToOrderMapperProfile002(IMappingEngine mappingEngine)
        {
            this.AutomapperEngine = mappingEngine;
        }

        protected override void Configure()
        {
            this.CreateMap<Domain.Order, Domain.Order>()
                .WithProfile("Profile002")
                .ForMember(dest => dest.Code, opt => opt.MapFrom(source => System.Guid.Empty))
                .ForMember(dest => dest.OrderLines, opt => opt.Ignore())
                .BeforeMap((source, destination) =>
                {
                    string message = "";
                })
                .AfterMap((source, destination) =>
                {
                    Console.WriteLine("OrderToOrderMapperProfile002 => Map For [Domain.Order] => [Profile002]");
                    foreach (var line in source.OrderLines)
                    {
                        destination.AddOrderLine(AutomapperEngine.Map<Domain.OrderLine, Domain.OrderLine>(line));
                    }

                });

            this.CreateMap<Domain.OrderLine, Domain.OrderLine>()
                .WithProfile("Profile002")
                .ForMember(dest => dest.Code, opt => opt.MapFrom(source => System.Guid.Empty))
                .ForMember(dest => dest.Order, opt => opt.Ignore())
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(source => 1))
                .AfterMap((source, destination) =>
                {
                    Console.WriteLine("OrderToOrderMapperProfile002 => Map For [Domain.OrderLine] => [Profile002]");
                    string itemCode = source.ItemCode;
                });

        }
    }
}
