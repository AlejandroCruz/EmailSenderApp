using EmailSenderApp.Domain.DataEntities;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSenderApp.DataInfrastructure.Repositories
{
    public class OrderRepository
    {
        OrderContext _orderContext;

        public OrderRepository(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }

        internal async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            try
            {
                var orderList = await _orderContext.Orders.AsNoTracking().ToListAsync();

                return orderList.ToList();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw;
            }
        }
    }
}
