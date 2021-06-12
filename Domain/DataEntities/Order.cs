using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailSenderApp.Domain.DataEntities
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string OrderNumber { get; set; }

        [Required]
        public string TransactionId { get; set; }

        [Required]
        public string DocumentId { get; set; }

        [Required]
        public decimal OrderAmount { get; set; }

        public decimal? OrderTax { get; set; }

        public DateTime? OrderDate { get; set; }

        [Required]
        public DateTime OrderRequestDate { get; set; }

        [Required]
        public DateTime OrderStarttime { get; set; }

        [Required]
        public DateTime OrderEndtime { get; set; }

        public bool? IsApproved { get; set; }

        public bool? IsRetrieved { get; set; }

        public string Message { get; set; } = null;

        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserEmail { get; set; }

        public string PickupName { get; set; } = null;

        public string FreightCode { get; set; } = null;

        [Required]
        [MaxLength(2)]
        public string StateCode { get; set; }

        public bool? Error { get; set; }

        [Required]
        [Timestamp]
        public DateTime CreatedTimestamp { get; set; } = DateTime.Now;

        public DateTime? DateModified { get; set; }
    }
}
