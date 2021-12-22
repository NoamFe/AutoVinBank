namespace AutoVin
{
    public class CorporateAccount : InvestmentAccount
    {
        public CorporateAccount(int id, string owner, decimal balance) 
            : base(id, owner, balance)
        {
        }

        public override InvestmentAccountType InvestmentAccountType => InvestmentAccountType.Corporate;
        public override decimal MaxWithdrawal => decimal.MaxValue; //coding challenge did not specify any limit, hence setting limit to max 
    }

}
