using System;
using System.ComponentModel.DataAnnotations;

namespace EmailSenderApp.Domain.DataEntities
{
    public class Order
    {
        [Key]
        public int ID { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
