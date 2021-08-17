using System.Text.Json.Serialization;

namespace EmailSenderApp.App.DTOs
{
    class ResponseDto
    {
        [JsonPropertyName("Amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("StateCode")]
        public string StateCode { get; set; }

        [JsonPropertyName("TransactionId")]
        public string TransactionId { get; set; }
        // TODO: [...]
    }
}
