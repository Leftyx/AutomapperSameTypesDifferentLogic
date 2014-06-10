using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomapperSameTypesDifferentLogic.Domain
{
    public class Order
    {
        public Order()
        {
            this._OrderLines = new HashSet<OrderLine>();
            this.DeliveryDate = DateTime.Now;
        }

        public Guid Code { get; set; }
        public string OrderNumber { get; set; }

        private DateTime DeliveryDate { get; set; }

        #region OrderLines

        private ICollection<OrderLine> _OrderLines = null;

        public virtual ReadOnlyCollection<OrderLine> OrderLines
        {
            get { return (new List<OrderLine>(_OrderLines).AsReadOnly()); }
        }

        public virtual bool AddOrderLine(OrderLine orderLine)
        {
            if (orderLine == null)
            {
                return (false);
            }
            if (!this._OrderLines.Contains(orderLine))
            {
                orderLine.Order = this;
                this._OrderLines.Add(orderLine);
                return (true);
            }
            return (false);
        }

        public virtual bool RemoveOrderLine(OrderLine orderLine)
        {
            if ((orderLine != null) && (this._OrderLines.Contains(orderLine)))
            {
                this._OrderLines.Remove(orderLine);
                orderLine.Order = null;
                return (true);
            }
            return (false);
        }

        #endregion
    }
}
