using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductApi.Models
{
    public class Product : Base
    {
        [JsonPropertyName("ProductId")]
        public Guid Id { get; set; }
        [JsonPropertyName("Price")]
        public decimal Price { get; set; }
        [JsonPropertyName("Currency")]
        public string Currency { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("ProductGroupId")]
        public Guid ProductGroupId { get; set; }
    }
}
