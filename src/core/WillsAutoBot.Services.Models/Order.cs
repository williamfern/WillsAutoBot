using System;

namespace WillsAutoBot.Services.Models
{
    public class Order
    {
        public string orderId { get; set; }
        public string marketId { get; set; }
        public string side { get; set; }
        public string type { get; set; }
        public DateTime creationTime { get; set; }
        public string price { get; set; }
        public string amount { get; set; }
        public string openAmount { get; set; }
        public string status { get; set; }
  
    }
}
