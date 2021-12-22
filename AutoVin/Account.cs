namespace AutoVin
{

    public abstract class Account
    {
        public Account(int id, string owner, decimal balance)
        {
            Id = id;
            Owner = owner;
            Balance = balance;
        }

        public int Id { get; set; }
        public string Owner { get; set; }
        public decimal Balance { get; private set; }

        public abstract decimal MaxWithdrawal { get; }

        public abstract AccountType AccountType { get; }

        public TransactionResponse Deposit(Transaction transaction)
        {  
            Balance += transaction.Amount;
            return new TransactionResponse
            {
                Success = true,
                TransactionId = transaction.TransactionId
            };
        }
        public TransactionResponse Withdraw(Transaction transaction)
        {
            if(transaction.Amount > Balance)
                return new TransactionResponse
                {
                    Success = false,
                    TransactionId = transaction.TransactionId,
                    ErrorMessage = "Not enough balance"
                };

            if (transaction.Amount > MaxWithdrawal)
            {
                return new TransactionResponse
                {
                    Success = false,
                    TransactionId = transaction.TransactionId,
                    ErrorMessage = "amount is higher than withdrawal limit"
                };
            }  
              
            Balance -= transaction.Amount;

            return new TransactionResponse
            {
                Success = true,
                TransactionId = transaction.TransactionId,
            };
        }
         
    }

}
