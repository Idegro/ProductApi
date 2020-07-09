using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Models
{
    public class RequestResult : IRequestResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
