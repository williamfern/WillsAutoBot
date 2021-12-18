using System.Net.Http.Headers;

namespace WillsAutoBot.BTCMarket.Services.Models
{
    public class ResponseModel
    {
        public HttpResponseHeaders Headers { get; set; }
        public string Content { get; set; }
    }
}
