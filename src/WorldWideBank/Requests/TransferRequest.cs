using WorldWideBank.Domain;

namespace WorldWideBank.Requests
{
    public class TransferRequest
    {
        public string SourceAccountNumber { get; set; }
        public string DestinationAccountNumber { get; set; }
        public Currency Currency { get; set; }
    }
}