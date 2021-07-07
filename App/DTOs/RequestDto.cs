using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderTaxProcessor.App.DTOs
{
    public interface IRequestDto
    {
        decimal Amount { get; set; }
        string StateCode { get; set; }
        string TransactionId { get; set; }
    }

    public class RequestDto : IRequestDto
    {
        [Required]
        [JsonPropertyName("Amount")]
        public decimal Amount { get; set; }

        [Required]
        [JsonPropertyName("StateCode")]
        public string StateCode { get; set; }

        [Required]
        [JsonPropertyName("TransactionId")]
        public string TransactionId { get; set; }
    }
}
