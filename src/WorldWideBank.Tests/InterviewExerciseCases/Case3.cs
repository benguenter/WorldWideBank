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
    public class Case3
    {
        [TestMethod]
        public void Bank_Withdraw_Transfer_And_Deposit_Succeeds()
        {
            var accountFactoryMock = new Mock<IAccountFactory>();
            accountFactoryMock
                .Setup(x => x.GetAccount(this._account1010.AccountNumber))
                .Returns(this._account1010);
            accountFactoryMock
                .Setup(x => x.GetAccount(this._account5500.AccountNumber))
                .Returns(this._account5500);

            var currencyActions = new CurrencyActions();
            var sut = new Bank(accountFactoryMock.Object, currencyActions);

            sut.Withdraw(new WithdrawRequest
            {
                Currency = new Currency
                {
                    Amount = 5000,
                    Type = CurrencyType.CAD
                },
                AccountNumber = this._account5500.AccountNumber
            }).AssertOk();
            Assert.AreEqual(10000, this._account5500.Balance.Amount);

            sut.Transfer(new TransferRequest
            {
                Currency = new Currency
                {
                    Amount = 7300,
                    Type = CurrencyType.CAD
                },
                SourceAccountNumber = this._account1010.AccountNumber,
                DestinationAccountNumber = this._account5500.AccountNumber
            }).AssertOk();
            Assert.AreEqual(125, this._account1010.Balance.Amount);
            Assert.AreEqual(17300, this._account5500.Balance.Amount);

            sut.Deposit(new DepositRequest
            {
                Currency = new Currency
                {
                    Amount = 13726,
                    Type = CurrencyType.MXN
                },
                AccountNumber = this._account1010.AccountNumber
            }).AssertOk();
            Assert.AreEqual(1497.6m, this._account1010.Balance.Amount);
        }

        private readonly Account _account1010 = new Account
        {
            AccountNumber = "1010",
            Balance = new Currency
            {
                Amount = 7425,
                Type = CurrencyType.CAD
            }
        };
        private readonly Account _account5500 = new Account
        {
            AccountNumber = "5500",
            Balance = new Currency
            {
                Amount = 15000,
                Type = CurrencyType.CAD
            }
        };
    }
}