namespace AutoVin
{
    public class IndividualAccount : InvestmentAccount
    {
        public IndividualAccount(int id, string owner, decimal balance) 
            : base(id, owner, balance)
        {
        }

        public override InvestmentAccountType InvestmentAccountType => InvestmentAccountType.Individual;
       
        public override decimal MaxWithdrawal => 500;
    }

}
