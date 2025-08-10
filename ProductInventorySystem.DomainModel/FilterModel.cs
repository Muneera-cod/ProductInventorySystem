using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductInventorySystem.DomainModel
{
    public class FilterModel
    {
        [JsonPropertyName("product_id")]
        public Guid ProductId { get; set; }

        [JsonPropertyName("stock_id")]
        public Guid StockId { get; set; }
    }
}
