using WorldWideBank.Domain;

namespace WorldWideBank.Requests
{
    public class DepositRequest
    {
        public string AccountNumber { get; set; }
        public Currency Currency { get; set; }
    }
}