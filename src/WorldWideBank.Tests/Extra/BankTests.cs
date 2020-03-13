using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WorldWideBank.Actions;
using WorldWideBank.Domain;
using WorldWideBank.Factories;
using WorldWideBank.Requests;

namespace WorldWideBank.Tests.Extra
{
    [TestClass]
    public class BankTests
    {
        [TestMethod]
        public void Bank_Withdraw_Insufficient_Funds_Fails()
        {
            var account0042 = new Account
            {
                AccountNumber = "0042",
                Balance = new Currency
                {
                    Amount = 100,
                    Type = CurrencyType.CAD
                }
            };
            var accountFactoryMock = new Mock<IAccountFactory>();
            accountFactoryMock
                .Setup(x => x.GetAccount(account0042.AccountNumber))
                .Returns(account0042);

            var currencyActions = new CurrencyActions();
            var sut = new Bank(accountFactoryMock.Object, currencyActions);

            var result = sut.Withdraw(new WithdrawRequest
            {
                Currency = new Currency
                {
                    Amount = 300,
                    Type = CurrencyType.USD
                },
                AccountNumber = account0042.AccountNumber
            });

            Assert.IsTrue(result.IsError);
            Assert.AreEqual("Cannot withdraw more than the balance of the account.", result.Errors.First());
        }

        [TestMethod]
        public void Bank_Withdraw_Currency_Conversion_Fails()
        {
            var account0042 = new Account
            {
                AccountNumber = "0042",
                Balance = new Currency
                {
                    Amount = 100,
                    Type = CurrencyType.MXN
                }
            };
            var accountFactoryMock = new Mock<IAccountFactory>();
            accountFactoryMock
                .Setup(x => x.GetAccount(account0042.AccountNumber))
                .Returns(account0042);

            var currencyActions = new CurrencyActions();
            var sut = new Bank(accountFactoryMock.Object, currencyActions);

            var result = sut.Withdraw(new WithdrawRequest
            {
                Currency = new Currency
                {
                    Amount = 300,
                    Type = CurrencyType.USD
                },
                AccountNumber = account0042.AccountNumber
            });

            Assert.IsTrue(result.IsError);
            Assert.AreEqual("We do not support converting USD to MXN.", result.Errors.First());
        }

        [TestMethod]
        public void Bank_Deposit_Currency_Conversion_Fails()
        {
            var account0042 = new Account
            {
                AccountNumber = "0042",
                Balance = new Currency
                {
                    Amount = 100,
                    Type = CurrencyType.MXN
                }
            };
            var accountFactoryMock = new Mock<IAccountFactory>();
            accountFactoryMock
                .Setup(x => x.GetAccount(account0042.AccountNumber))
                .Returns(account0042);

            var currencyActions = new CurrencyActions();
            var sut = new Bank(accountFactoryMock.Object, currencyActions);

            var result = sut.Deposit(new DepositRequest
            {
                Currency = new Currency
                {
                    Amount = 300,
                    Type = CurrencyType.USD
                },
                AccountNumber = account0042.AccountNumber
            });

            Assert.IsTrue(result.IsError);
            Assert.AreEqual("We do not support converting USD to MXN.", result.Errors.First());
        }

        [TestMethod]
        public void Bank_Transfer_Currency_Conversion_Fails()
        {
            var account0042 = new Account
            {
                AccountNumber = "0042",
                Balance = new Currency
                {
                    Amount = 100,
                    Type = CurrencyType.MXN
                }
            };
            var accountFactoryMock = new Mock<IAccountFactory>();
            accountFactoryMock
                .Setup(x => x.GetAccount(account0042.AccountNumber))
                .Returns(account0042);

            var currencyActions = new CurrencyActions();
            var sut = new Bank(accountFactoryMock.Object, currencyActions);

            var result = sut.Transfer(new TransferRequest
            {
                Currency = new Currency
                {
                    Amount = 300,
                    Type = CurrencyType.USD
                },
                SourceAccountNumber = account0042.AccountNumber,
                DestinationAccountNumber = account0042.AccountNumber
            });

            Assert.IsTrue(result.IsError);
            Assert.AreEqual("We do not support converting USD to MXN.", result.Errors.First());
        }
    }
}