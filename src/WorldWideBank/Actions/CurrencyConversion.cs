using WorldWideBank.Domain;

namespace WorldWideBank.Actions
{
    /// <summary>
    /// Central place to convert an amount from one <see cref="CurrencyType"/> to another <see cref="CurrencyType"/>
    /// </summary>
    /// TODO: In a real world scenario, the conversion rates would be loaded from some configuration or external API.
    /// TODO: Given the simplicity of this test, we will use a static file for now with the thought that this will be
    /// TODO: injected via DI with dependencies at some point in the future.
    public static class CurrencyConversion
    {
        /// <summary>
        /// Converts currency from the sourceCurrency to the destinationCurrency and returns the result.
        /// If the currency conversion is not supported, an error is returned.
        /// </summary>
        public static Result<Currency> Convert(decimal amount, CurrencyType sourceCurrency, CurrencyType destinationCurrency)
        {
            return (sourceCurrency, destinationCurrency) switch
            {
                (CurrencyType.USD, CurrencyType.CAD) => Result.Ok(
                    new Currency {Amount = amount * 2m, Type = CurrencyType.CAD}),
                (CurrencyType.CAD, CurrencyType.USD) => Result.Ok(
                    new Currency {Amount = amount / 2m, Type = CurrencyType.USD}),
                (CurrencyType.MXN, CurrencyType.CAD) => Result.Ok(
                    new Currency {Amount = amount / 10m, Type = CurrencyType.CAD}),
                (CurrencyType.CAD, CurrencyType.MXN) => Result.Ok(
                    new Currency {Amount = amount * 10m, Type = CurrencyType.MXN}),
                (CurrencyType.CAD, CurrencyType.CAD) => Result.Ok(
                    new Currency {Amount = amount, Type = CurrencyType.CAD}),
                _ => Result.Error<Currency>(
                    $"We do not support converting {sourceCurrency} to {destinationCurrency}.")
            };
        }
    }
}