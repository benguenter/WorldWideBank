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
    public class Case2
    {
        [TestMethod]
        public void Bank_Deposit_Plus_Withdraw_Succeeds()
        {
            var accountFactoryMock = new Mock<IAccountFactory>();
            accountFactoryMock
                .Setup(x => x.GetAccount(this._account2001.AccountNumber))
                .Returns(this._account2001);

            var currencyActions = new CurrencyActions();
            var sut = new Bank(accountFactoryMock.Object, currencyActions);

            sut.Withdraw(new WithdrawRequest
            {
                Currency = new Currency
                {
                    Amount = 5000,
                    Type = CurrencyType.MXN
                },
                AccountNumber = this._account2001.AccountNumber
            }).AssertOk();
            Assert.AreEqual(34500, this._account2001.Balance.Amount);

            sut.Withdraw(new WithdrawRequest
            {
                Currency = new Currency
                {
                    Amount = 12500,
                    Type = CurrencyType.USD
                },
                AccountNumber = this._account2001.AccountNumber
            }).AssertOk();
            Assert.AreEqual(9500, this._account2001.Balance.Amount);

            sut.Deposit(new DepositRequest
            {
                Currency = new Currency
                {
                    Amount = 300,
                    Type = CurrencyType.CAD
                },
                AccountNumber = this._account2001.AccountNumber
            }).AssertOk();
            Assert.AreEqual(9800, this._account2001.Balance.Amount);
        }

        private readonly Account _account2001 = new Account
        {
            AccountNumber = "2001",
            Balance = new Currency
            {
                Amount = 35000,
                Type = CurrencyType.CAD
            }
        };
    }
}