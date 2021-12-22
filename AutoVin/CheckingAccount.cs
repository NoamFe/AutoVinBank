namespace AutoVin
{ 
    public class CheckingAccount : Account
    {
        public CheckingAccount(int id, string owner, decimal balance) 
            : base(id, owner, balance)
        {
        }

        public override AccountType AccountType => AccountType.Checking;
       
        public override decimal MaxWithdrawal => decimal.MaxValue;//coding challenge did not specify any limit, hence setting limit to max 
    }

}
