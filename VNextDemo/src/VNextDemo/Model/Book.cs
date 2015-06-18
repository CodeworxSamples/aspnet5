using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VNextDemo.Model
{
    public class Book
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid Id
        { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title
        { get; set; }

        [JsonProperty(PropertyName = "price")]
        public decimal Price
        { get; set; }
    }
}
