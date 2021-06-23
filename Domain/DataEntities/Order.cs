using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailSenderApp.Domain.DataEntities
{
    [Table("Orders")]
    public class Order
    {
        // Property line position => column order
        public int ID { get; set; }
        public string OrderNumber { get; set; }
        public string DocumentNumber { get; set; }
        public string PayTransNumber { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal? OrderTax { get; set; }
        public string StateCode { get; set; }
        public char FreightCode { get; set; }
        public bool? IsRetrieved { get; set; }
        public bool? IsPayProcessed { get; set; }
        public bool? IsApproved { get; set; }
        public bool? Error { get; set; }
        public string TransMessage { get; set; } = null;
        public DateTime CreatedDate { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime FreightDate { get; set; }
        public TimeSpan FreightStarttime { get; set; }
        public TimeSpan FreightEndtime { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string PickupName { get; set; }
    }
}
