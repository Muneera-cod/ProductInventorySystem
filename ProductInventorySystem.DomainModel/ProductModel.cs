using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductInventorySystem.DomainModel
{
    public class ProductModel
    {
        [JsonPropertyName("product_id")]
        public Guid ProductId { get; set; }

        [JsonPropertyName("product_code")]
        public string ProductCode { get; set; }

        [Required]
        [JsonPropertyName("product_name")]
        public string ProductName { get; set; }

        [JsonPropertyName("product_image")]
        public byte[] ProductImage { get; set; }

        [JsonPropertyName("created_date")]
        public DateTimeOffset? CreatedDate { get; set; }

        [JsonPropertyName("updated_date")]
        public DateTimeOffset? UpdatedDate { get; set; }

        [JsonPropertyName("created_user")]
        public Guid CreatedUser { get; set; }

        [JsonPropertyName("is_favourite")]
        public bool IsFavourite { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("hsn_code")]
        public string HSNCode { get; set; }

        [Required]
        public List<Variant> Variants { get; set; } = new List<Variant>();

    }
    public class Variant
    {
        [Required]
        [JsonPropertyName("name")]
        public string VariantName { get; set; }

        [Required, MinLength(1)]
        [JsonPropertyName("options")]
        public List<string> SubVariants { get; set; } = new List<string>();
    }


}
