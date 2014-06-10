using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomapperSameTypesDifferentLogic.Domain
{
    public class OrderLine
    {
        public Order Order { get; set; }

        public Guid Code { get; set; }
        public string ItemCode { get; set; }
        public decimal Quantity { get; set; }
    }
}
