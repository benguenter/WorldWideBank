using WorldWideBank.Domain;

namespace WorldWideBank.Actions
{
    public class CurrencyActions : ICurrencyActions
    {
        /// <inheritdoc />
        public Result<Currency> Add(Currency left, Currency right)
        {
            var convertedCurrency = CurrencyConversion.Convert(right.Amount, right.Type, left.Type);
            if (convertedCurrency.IsError)
            {
                return convertedCurrency;
            }

            left.Amount += convertedCurrency.Value.Amount;
            return Result.Ok(left);
        }

        /// <inheritdoc />
        public Result<Currency> Subtract(Currency left, Currency right)
        {
            var convertedRightCurrency = CurrencyConversion.Convert(right.Amount, right.Type, left.Type);
            if (convertedRightCurrency.IsError)
            {
                return convertedRightCurrency;
            }

            left.Amount -= convertedRightCurrency.Value.Amount;
            return Result.Ok(left);
        }
    }

    public interface ICurrencyActions
    {
        /// <summary>
        /// Always converts the <param name="right"> </param> <see cref="CurrencyType"/> to the <param name="left"/> <see cref="CurrencyType"/>
        /// and adds the result to the left <see cref="Currency.Amount"/>.
        /// </summary>
        Result<Currency> Add(Currency left, Currency right);

        /// <summary>
        /// Always converts the <param name="right"> </param> <see cref="CurrencyType"/> to the <param name="left"/> <see cref="CurrencyType"/>
        /// and subtracts the result from the <param name="left"></param> <see cref="Currency.Amount"/>.
        /// </summary>
        Result<Currency> Subtract(Currency left, Currency right);
    }
}