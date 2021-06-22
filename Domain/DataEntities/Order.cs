using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailSenderApp.Domain.DataEntities
{
    [Table("Orders")]
    public class Order
    {
        // Property order corresponds to column order
        public int ID { get; set; }
        public string OrderNumber { get; set; }
        public string DocumentNumber { get; set; }
        public string TransNumber { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal? OrderTax { get; set; }
        public string StateCode { get; set; }
        public bool? IsRetrieved { get; set; }
        public bool? IsApproved { get; set; }
        public bool? Error { get; set; }
        public string TransMessage { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime OrderRequestDate { get; set; }
        public DateTime OrderStarttime { get; set; }
        public DateTime OrderEndtime { get; set; }
        public string FreightCode { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string PickupName { get; set; }
    }
}
