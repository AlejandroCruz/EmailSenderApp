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
                        OrderNumber = "1001",
                        DocumentNumber = "1001-A",
                        OrderAmount = 1.99M,
                        StateCode = "PR",
                        OrderDate = DateTime.Parse("6/5/2021"),
                        FreightDate = DateTime.Parse("6/6/2021"),
                        FreightStarttime = TimeSpan.Parse("8:00"),
                        FreightEndtime = TimeSpan.Parse("11:00"),
                        UserName = "Bilko Bellington",
                        UserEmail = "mail@mail",
                        PickupName = "Polar Bear"
                    },
                    new Order
                    {
                        OrderNumber = "1001",
                        DocumentNumber = "1001-B",
                        OrderAmount = 10M,
                        StateCode = "PR",
                        OrderDate = DateTime.Parse("6/5/2021"),
                        FreightDate = DateTime.Parse("6/6/2021"),
                        FreightStarttime = TimeSpan.Parse("11:00"),
                        FreightEndtime = TimeSpan.Parse("13:30"),
                        UserName = "Bilko Bellington",
                        UserEmail = "mail@mail",
                        PickupName = "Polar Bear"
                    },
                    new Order
                    {
                        OrderNumber = "1002",
                        DocumentNumber = "1002-A",
                        OrderAmount = 1000.99M,
                        StateCode = "fl",
                        OrderDate = DateTime.Parse("6/22/2021"),
                        FreightDate = DateTime.Parse("6/22/2021"),
                        FreightStarttime = TimeSpan.Parse("10:0"),
                        FreightEndtime = TimeSpan.Parse("17:50"),
                        UserName = "Mr. Porter",
                        UserEmail = "mporter@mail",
                        PickupName = "Mr. Porter"
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

        internal async Task ExecuteDbFunction(string query)
        {
            try
            {
                await _orderContext.Database.ExecuteSqlRawAsync(query);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw;
            }
        }
    }
}
