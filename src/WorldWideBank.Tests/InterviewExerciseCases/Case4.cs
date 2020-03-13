using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WorldWideBank.Actions;
using WorldWideBank.Domain;
using WorldWideBank.Factories;
using WorldWideBank.Requests;
using WorldWideBank.Tests.Extensions;

namespace WorldWideBank.Tests.InterviewExerciseCases
{
    [TestClass]
    public class Case4
    {
        [TestMethod]
        public void Bank_Withdraw_Deposit_And_Transfer_Succeeds()
        {
            var accountFactoryMock = new Mock<IAccountFactory>();
            accountFactoryMock
                .Setup(x => x.GetAccount(this._account0123.AccountNumber))
                .Returns(this._account0123);
            accountFactoryMock
                .Setup(x => x.GetAccount(this._account0456.AccountNumber))
                .Returns(this._account0456);

            var currencyActions = new CurrencyActions();
            var sut = new Bank(accountFactoryMock.Object, currencyActions);

            sut.Withdraw(new WithdrawRequest
            {
                Currency = new Currency
                {
                    Amount = 70,
                    Type = CurrencyType.USD
                },
                AccountNumber = this._account0123.AccountNumber
            }).AssertOk();
            Assert.AreEqual(10, this._account0123.Balance.Amount);

            sut.Deposit(new DepositRequest
            {
                Currency = new Currency
                {
                    Amount = 23789,
                    Type = CurrencyType.USD
                },
                AccountNumber = this._account0456.AccountNumber
            }).AssertOk();
            Assert.AreEqual(112578, this._account0456.Balance.Amount);

            sut.Transfer(new TransferRequest
            {
                Currency = new Currency
                {
                    Amount = 23.75m,
                    Type = CurrencyType.CAD
                },
                SourceAccountNumber = this._account0456.AccountNumber,
                DestinationAccountNumber = this._account0123.AccountNumber
            }).AssertOk();
            Assert.AreEqual(33.75m, this._account0123.Balance.Amount);
            Assert.AreEqual(112554.25m, this._account0456.Balance.Amount);
        }

        private readonly Account _account0123 = new Account
        {
            AccountNumber = "0123",
            Balance = new Currency
            {
                Amount = 150,
                Type = CurrencyType.CAD
            }
        };
        private readonly Account _account0456 = new Account
        {
            AccountNumber = "0456",
            Balance = new Currency
            {
                Amount = 65000,
                Type = CurrencyType.CAD
            }
        };
    }
}