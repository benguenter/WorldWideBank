using WorldWideBank.Domain;

namespace WorldWideBank.Requests
{
    public class WithdrawRequest
    {
        public string AccountNumber { get; set; }
        public Currency Currency { get; set; }
    }
}