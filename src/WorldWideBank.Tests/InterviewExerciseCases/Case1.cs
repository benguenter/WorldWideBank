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
    public class Case1
    {
        [TestMethod]
        public void Bank_Deposit_Succeeds()
        {
            var accountFactoryMock = new Mock<IAccountFactory>();
            accountFactoryMock
                .Setup(x => x.GetAccount(this._account1234.AccountNumber))
                .Returns(this._account1234);

            var currencyActions = new CurrencyActions();
            var sut = new Bank(accountFactoryMock.Object, currencyActions);

            sut.Deposit(new DepositRequest
            {
                Currency = new Currency
                {
                    Amount = 300,
                    Type = CurrencyType.USD
                },
                AccountNumber = this._account1234.AccountNumber
            }).AssertOk();

            Assert.AreEqual(700, this._account1234.Balance.Amount);
        }

        private readonly Account _account1234 = new Account
        {
            AccountNumber = "1234",
            Balance = new Currency
            {
                Amount = 100,
                Type = CurrencyType.CAD
            }
        };
    }
}