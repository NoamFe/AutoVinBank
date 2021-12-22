namespace AutoVin
{ 
    public abstract class InvestmentAccount : Account
    {
        protected InvestmentAccount(int id, string owner, decimal balance) 
            : base(id, owner, balance)
        {
        }

        public abstract InvestmentAccountType InvestmentAccountType { get; }
     
        public override AccountType AccountType => AccountType.Investment;
    } 
}
