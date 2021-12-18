using System.Reflection.Metadata.Ecma335;

namespace WillsAutoBot.BTCMarket.Services.Models
{
    public class OrderRequest
    {
        public string MarketId { get; set; }
        public int Before { get; set; }
        public int After { get; set; }
        public int Limit { get; set; }
        public string Status { get; set; }
    }
}