using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductInventorySystem.DomainModel
{
    public  class Stock
    {
        [JsonPropertyName("stock_id")]
        public Guid StockId { get; set; }

        [JsonPropertyName("fk_product")]
        public Guid FK_Product { get; set; }

        [JsonPropertyName("fk_variant")]

        public Guid FK_Variant { get; set; }

        [JsonPropertyName("fk_subvariant")]

        public Guid FK_Subvariant { get; set; }

        [JsonPropertyName("quatity")]
        public int quatity { get; set; }
       
    }
}
