namespace EmailSenderApp.App.DTOs
{
    class ResponseDto
    {
        public string TransactionId { get; set; }
        public string Message { get; set; }
        public decimal TaxedAmount { get; set; }
        // TODO: [...]
    }
}
