namespace AutoVin
{
    public class TransactionResponse
    {
        public Guid TransactionId  { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }

}
