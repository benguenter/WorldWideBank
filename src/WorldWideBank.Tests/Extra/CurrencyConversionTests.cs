using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WorldWideBank.Actions;
using WorldWideBank.Domain;
using WorldWideBank.Factories;
using WorldWideBank.Requests;
using WorldWideBank.Tests.Extensions;

namespace WorldWideBank.Tests.Extra
{
    [TestClass]
    public class CurrencyConversionTests
    {
        [TestMethod]
        public void CurrencyConversion_From_CAD_To_USD_Succeeds()
        {
            var result = CurrencyConversion.Convert(
                    42,
                    CurrencyType.CAD,
                    CurrencyType.USD)
                .AssertOk();
            Assert.AreEqual(21, result.Amount);
        }

        [TestMethod]
        public void CurrencyConversion_From_USD_To_CAD_Succeeds()
        {
            var result = CurrencyConversion.Convert(
                    42,
                    CurrencyType.USD,
                    CurrencyType.CAD)
                .AssertOk();
            Assert.AreEqual(84, result.Amount);
        }

        [TestMethod]
        public void CurrencyConversion_From_MXN_To_CAD_Succeeds()
        {
            var result = CurrencyConversion.Convert(
                    42,
                    CurrencyType.MXN,
                    CurrencyType.CAD)
                .AssertOk();
            Assert.AreEqual(4.2m, result.Amount);
        }

        [TestMethod]
        public void CurrencyConversion_From_CAD_To_MXN_Succeeds()
        {
            var result = CurrencyConversion.Convert(
                    42,
                    CurrencyType.CAD,
                    CurrencyType.MXN)
                .AssertOk();
            Assert.AreEqual(420, result.Amount);
        }

        [TestMethod]
        public void CurrencyConversion_From_USD_To_MXN_Fails()
        {
            var result = CurrencyConversion.Convert(
                42,
                CurrencyType.USD,
                CurrencyType.MXN);
            Assert.IsTrue(result.IsError);
        }
    }
}