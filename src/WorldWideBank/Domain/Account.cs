namespace WorldWideBank.Domain
{
    public class Account
    {
        /// <summary>
        /// Balance is always in <see cref="CurrencyType.CAD"/>.
        /// </summary>
        public Currency Balance { get; set; }
        public string AccountNumber { get; set; }
    }
}