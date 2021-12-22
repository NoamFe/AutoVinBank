namespace AutoVin
{
    public class Transaction
    {
        public Guid TransactionId;

        public int AccountId;

        public decimal Amount { get; set; }
    }

    public class TransferTransaction : Transaction
    {  
        public int TargetAccountId;
    }
}
