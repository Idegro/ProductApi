using System;
using System.Text.Json.Serialization;

namespace ProductApi.Models
{
    public class Product : Base
    {
        [JsonPropertyName("ProductId")]
        public new Guid Id { get; set; }
        [JsonPropertyName("Price")]
        public decimal Price { get; set; }
        [JsonPropertyName("Currency")]
        public string Currency { get; set; }
        [JsonPropertyName("Name")]
        public new string Name { get; set; }
        [JsonPropertyName("ProductGroupId")]
        public Guid ProductGroupId { get; set; }
    }
}
