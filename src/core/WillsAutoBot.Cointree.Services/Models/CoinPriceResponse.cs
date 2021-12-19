using System;
using System.Text.Json.Serialization;

namespace WillsAutoBot.Cointree.Services.Models
{
    public class CoinPriceResponse
    {
        [JsonPropertyName("ask")] public decimal Ask { get; set; }

        [JsonPropertyName("bid")] public decimal Bid { get; set; }

        [JsonPropertyName("rate")] public decimal Rate { get; set; }

        [JsonPropertyName("sell")] public string Sell { get; set; }

        [JsonPropertyName("buy")] public string Buy { get; set; }

        [JsonPropertyName("spotRate")] public decimal SpotRate { get; set; }

        [JsonPropertyName("market")] public string Market { get; set; }

        [JsonPropertyName("timestamp")] public DateTimeOffset Timestamp { get; set; }

        [JsonPropertyName("rateType")] public string RateType { get; set; }
    }
}