namespace AutoVin
{
    public class Bank
    {
        public string BankName { get; set; }

        public IList<Account> Accounts { get; set; }
         

        public bool Deposit(Transaction transaction)
        {
            var account = Accounts.FirstOrDefault(e => e.Id == transaction.AccountId);

            if (account == null)
                throw new Exception("cannot find account");

            var result = account.Deposit(transaction);

            return result.Success;
        }

        public bool Withdraw(Transaction transaction)
        {
            var account = Accounts.FirstOrDefault(e => e.Id == transaction.AccountId);

            if (account == null)
                throw new Exception("cannot find account");

            var result = account.Withdraw(transaction);
          
            return result.Success;
        }

        public bool Transfer(TransferTransaction transaction)
        {
            var account = Accounts.FirstOrDefault(e => e.Id == transaction.AccountId);

            if (account == null)
                throw new Exception("cannot find account");

            var targetAccount = Accounts.FirstOrDefault(e => e.Id == transaction.TargetAccountId);

            if (targetAccount == null)
                throw new Exception("cannot find target account");


            var result = account.Withdraw(transaction);

            if (result.Success)
            {
                targetAccount.Deposit(transaction);
                return true;
            }

            return false;
        }
    }

}
