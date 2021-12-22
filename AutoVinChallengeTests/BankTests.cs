using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AutoVin.Tests
{
    public class BankTests
    {
        private Bank Bank { get; set; }

        public BankTests()
        {
            Bank = new Bank();
            Bank.BankName = "testBank";
            Bank.Accounts = new List<Account>();

            Bank.Accounts.Add(new IndividualAccount(1, "Joe", new decimal(505.52)));
            Bank.Accounts.Add(new IndividualAccount(2, "Chris", new decimal(499.99)));

            Bank.Accounts.Add(new CorporateAccount(3, "Jill", new decimal(499.99)));
            Bank.Accounts.Add(new CorporateAccount(4, "Henry", new decimal(505.99)));

            Bank.Accounts.Add(new CheckingAccount(5, "Jessica", new decimal(499.95)));
            Bank.Accounts.Add(new CheckingAccount(6, "Craig", new decimal(501.88)));
        }


        [Fact]
        public void Deposit_ShouldAddToBalance_ForAllAccountTypes()
        { 
            foreach (var account in Bank.Accounts)
            {
                var beforeBalance = account.Balance;

                var amount = new decimal(151.18);
               
                Bank.Deposit(new Transaction { AccountId = account.Id, Amount = amount });

                account.Balance.ShouldBe(beforeBalance + amount);
            } 
        }


        [Fact]
        public void Withdraw_ShouldNotRemoveAmountFromBalance_WhenAmountIsBiggerThanBalance()
        {
            foreach (var account in Bank.Accounts)
            {
                var beforeBalance = account.Balance;

                var amount = beforeBalance + 0.01m;

                var response = Bank.Withdraw(new Transaction { AccountId = account.Id, Amount = amount });

                response.ShouldBeFalse();
                account.Balance.ShouldBe(beforeBalance);
            }
        }

        [Fact]
        public void Withdraw_ShouldRemoveAmountFromBalance_WhenAmountIsSmallerThanBalance()
        {
            foreach (var account in Bank.Accounts)
            {
                var beforeBalance = account.Balance;

                var amount = 0.03m;

                var response = Bank.Withdraw(new Transaction { AccountId = account.Id, Amount = amount });

                response.ShouldBeTrue();
                account.Balance.ShouldBe(beforeBalance - amount);
            }
        }

        [Fact]
        public void Withdraw_ShouldNotRemoveAmountFromBalance_WhenExceedingindIvidualAccountLimit()
        {

            var individualAccount = Bank.Accounts[0];

            var beforeBalance = individualAccount.Balance;
             
            var response = Bank.Withdraw(new Transaction { AccountId = individualAccount.Id, Amount = 501 });

            response.ShouldBeFalse();
            individualAccount.Balance.ShouldBe(beforeBalance);
        }



        [Fact]
        public void Transfer_ShouldMoveFunds_WhenAmountExistInSourceAccount()
        { 
            var sourceAccount = Bank.Accounts[1];
            var targetAccount = Bank.Accounts[3];

            var sourceAccountBeforeBalance = sourceAccount.Balance;
            var targetAccountBeforeBalance = targetAccount.Balance;
            var amount = 20.6m;

            var response = Bank.Transfer(new TransferTransaction 
            { 
                AccountId = sourceAccount.Id,
                Amount = amount,
                TargetAccountId = targetAccount.Id
            });

            response.ShouldBeTrue(); 
            sourceAccount.Balance.ShouldBe(sourceAccountBeforeBalance - amount);
            targetAccount.Balance.ShouldBe(targetAccountBeforeBalance + amount);
        }


        [Fact]
        public void Transfer_ShouldNotMoveFunds_WhenAmountDoesNotExistInSourceAccount()
        {
            var sourceAccount = Bank.Accounts[1];
            var targetAccount = Bank.Accounts[3];

            var sourceAccountBeforeBalance = sourceAccount.Balance;
            var targetAccountBeforeBalance = targetAccount.Balance;
            var amount = sourceAccountBeforeBalance + 0.01m;

            var response = Bank.Transfer(new TransferTransaction
            {
                AccountId = sourceAccount.Id,
                Amount = amount,
                TargetAccountId = targetAccount.Id
            });

            response.ShouldBeFalse();
            sourceAccount.Balance.ShouldBe(sourceAccountBeforeBalance);
            targetAccount.Balance.ShouldBe(targetAccountBeforeBalance);
        }
    }
}