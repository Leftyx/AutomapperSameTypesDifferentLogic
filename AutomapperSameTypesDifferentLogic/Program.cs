using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomapperSameTypesDifferentLogic
{
    using AutoMapper;
    using AutomapperSameTypesDifferentLogic.Infrastructure.DependencyResolution;
    using AutomapperSameTypesDifferentLogic.Infrastructure.Mapping;
    using StructureMap;

    class Program
    {
        static void Main(string[] args)
        {
            var container = IoC.Initialize();

            // TestStandardEngine(container);
            TestCustomEngine(container);

            //TestStandardEngine(container);
            //TestCustomEngine(container);

            Console.WriteLine("Finished!");
            Console.ReadLine();

        }

        private static void TestStandardEngine(IContainer container)
        {
            Domain.Order order = CreateOrderAndLines();

            Console.WriteLine(order.Code);

            var mapper = container.GetInstance<IMappingEngine>("StandardMappingEngine");

            order = mapper.Map<Domain.Order, Domain.Order>(order);

            Console.WriteLine(order.Code);
            foreach (var line in order.OrderLines)
            {
                Console.WriteLine(line.Code);
                Console.WriteLine(line.Quantity);
            }
        }

        private static void TestCustomEngine(IContainer container)
        {
            Domain.Order order = CreateOrderAndLines();

            Console.WriteLine(order.Code);

            var mapper = container.GetInstance<IMappingEngine>("CustomMappingEngine");

            Domain.Order orderMerge = CreateOrderAndLinesTemp();

            order = mapper.Map<Domain.Order, Domain.Order>(order, orderMerge);

            Console.WriteLine(order.Code);
            foreach (var line in order.OrderLines)
            {
                Console.WriteLine(line.Code);
                Console.WriteLine(line.Quantity);
            }
        }

        private static Domain.Order CreateOrderAndLines()
        {
            Domain.Order order = new Domain.Order();
            order.Code = Guid.NewGuid();
            order.OrderNumber = "VI13001020";

            Domain.OrderLine orderLine1 = new Domain.OrderLine();
            orderLine1.Code = Guid.NewGuid();
            orderLine1.ItemCode = "IT60N";
            orderLine1.Quantity = 200;
            order.AddOrderLine(orderLine1);

            Domain.OrderLine orderLine2 = new Domain.OrderLine();
            orderLine2.Code = Guid.NewGuid();
            orderLine2.ItemCode = "IT80N";
            orderLine2.Quantity = 210;
            order.AddOrderLine(orderLine2);

            return (order);

        }

        private static Domain.Order CreateOrderAndLinesTemp()
        {
            Domain.Order order = new Domain.Order();
            order.Code = Guid.NewGuid();
            order.OrderNumber = "XXX";

            Domain.OrderLine orderLine1 = new Domain.OrderLine();
            orderLine1.Code = Guid.NewGuid();
            orderLine1.ItemCode = "XXX";
            orderLine1.Quantity = 0;
            order.AddOrderLine(orderLine1);

            return (order);

        }
    }
}
