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
    [Serializable()]
    public class Product : Base
    {
        [JsonProperty("ProductId")]
        public Guid Id { get; set; }

        public decimal Price { get; set; }

        public string Currency { get; set; }

        public string Name { get; set; }

        public Guid ProductGroupId { get; set; }
    }
}
