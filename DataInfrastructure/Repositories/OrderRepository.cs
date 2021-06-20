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

        internal async Task InsertTestData()
        {
            try
            {
                Order[] orders = new Order[]
                {
                    new Order
                    {
                        OrderNumber = "101",
                        TransactionId = "1A",
                        DocumentId = "101-A",
                        OrderAmount = 1.99M,
                        OrderDate = DateTime.Parse("6/5/2021"),
                        OrderRequestDate = DateTime.Parse("6/10/2021 12:00:00 AM"),
                        OrderStarttime = DateTime.Parse("6/10/2021 8:00:00 AM"),
                        OrderEndtime = DateTime.Parse("6/10/2021 11:00:00 AM"),
                        UserName = "Bilko Bellington",
                        UserEmail = "mail@mail",
                        PickupName = "Polar Bear",
                        StateCode = "PR"
                    },
                    new Order
                    {
                        OrderNumber = "101",
                        TransactionId = "2A",
                        DocumentId = "101-B",
                        OrderAmount = 10M,
                        OrderDate = DateTime.Parse("6/5/2021 9:00:00 AM"),
                        OrderRequestDate = DateTime.Parse("6/10/2021 12:00:00 AM"),
                        OrderStarttime = DateTime.Parse("6/10/2021 8:00:00 AM"),
                        OrderEndtime = DateTime.Parse("6/10/2021 11:00:00 AM"),
                        UserName = "Bilko Bellington",
                        UserEmail = "mail@mail",
                        PickupName = "Polar Bear",
                        StateCode = "PR"
                    },
                    new Order
                    {
                        OrderNumber = "202",
                        TransactionId = "1A",
                        DocumentId = "101-A",
                        OrderAmount = 15M,
                        OrderDate = DateTime.Parse("6/1/2021 9:00:00 PM"),
                        OrderRequestDate = DateTime.Parse("6/2/2021 12:00:00 AM"),
                        OrderStarttime = DateTime.Parse("6/3/2021 8:00:00 PM"),
                        OrderEndtime = DateTime.Parse("6/3/2021 11:00:00 PM"),
                        UserName = "Mr. Porter",
                        UserEmail = "mporter@mail",
                        StateCode = "FL"
                    }
                };
                foreach (Order order in orders)
                {
                    _orderContext.Orders.Add(order);
                    await _orderContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                throw;
            }
        }
    }
}
