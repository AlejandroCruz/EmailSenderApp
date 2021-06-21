using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailSenderApp.Domain.DataEntities
{
    [Table("Orders")]
    public class Order
    {
        public int ID { get; set; }

        public bool? Error { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsRetrieved { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime OrderEndtime { get; set; }
        public DateTime OrderRequestDate { get; set; }
        public DateTime OrderStarttime { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal? OrderTax { get; set; }
        public string DocumentId { get; set; }
        public string FreightCode { get; set; } = null;
        public string Message { get; set; } = null;
        public string OrderNumber { get; set; }
        public string PickupName { get; set; } = null;
        public string StateCode { get; set; }
        public string TransactionId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
    }
}
