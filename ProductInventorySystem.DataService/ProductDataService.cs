using Microsoft.Data.SqlClient;
using ProductInventorySystem.DomainModel;
using System.Data;

namespace ProductInventorySystem.DataService
{

    public class ProductDataService
    {
        public string _connectionString { get; set; } = string.Empty;
        public ProductDataService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public HttpResponseModel AddProduct(ProductJson productJson)
        {
            HttpResponseModel httpResponseModel = new HttpResponseModel();
            Procedures procedures = new Procedures();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = procedures.SP_Product_InsertProduct; 
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@JsonData", productJson.JSON);

                connection.Open();
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            httpResponseModel.ResponseCode = reader["ResponseCode"] != DBNull.Value ? Convert.ToInt32(reader["ResponseCode"]) : 0;
                            httpResponseModel.ResponseMessage = reader["ResponseMessage"]?.ToString();
                            httpResponseModel.ResponseID = reader["ResponseStatus"] != DBNull.Value ? Convert.ToInt32(reader["ResponseStatus"]) : 0;
                        }
                    }
                    return httpResponseModel;

                }
                catch
                {
                    return httpResponseModel;
                }
            }

        }
        public HttpResponseModel AddStock(ProductJson productJson)
        {
            HttpResponseModel httpResponseModel = new HttpResponseModel();
            Procedures procedures = new Procedures();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = procedures.SP_Product_InsertStock;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@JsonData", productJson.JSON);

                connection.Open();
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            httpResponseModel.ResponseCode = reader["ResponseCode"] != DBNull.Value ? Convert.ToInt32(reader["ResponseCode"]) : 0;
                            httpResponseModel.ResponseMessage = reader["ResponseMessage"]?.ToString();
                            httpResponseModel.ResponseID = reader["ResponseStatus"] != DBNull.Value ? Convert.ToInt32(reader["ResponseStatus"]) : 0;
                        }
                    }
                    return httpResponseModel;

                }
                catch
                {
                    return httpResponseModel;
                }
            }
        }

        public List<ProductModel> SelectProduct(FilterModel filter)
        {
            var products = new List<ProductModel>();
            var procedures = new Procedures();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = procedures.SP_Product_GetProductsAndStock;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@stock_id", filter.StockId);
                command.Parameters.AddWithValue("@product_id", filter.ProductId);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Guid productId = reader["product_id"] != DBNull.Value
                            ? reader.GetGuid(reader.GetOrdinal("product_id"))
                            : Guid.Empty;

                        var product = products.FirstOrDefault(p => p.ProductId == productId);
                        if (product == null)
                        {
                            product = new ProductModel
                            {
                                ProductId = productId,
                                ProductName = reader["name"]?.ToString() ?? string.Empty,
                                Variants = new List<Variant>()
                            };
                            products.Add(product);
                        }

                        string variantName = reader["variant"]?.ToString();
                        if (!string.IsNullOrWhiteSpace(variantName))
                        {
                            var variant = product.Variants.FirstOrDefault(v => v.VariantName == variantName);
                            if (variant == null)
                            {
                                variant = new Variant
                                {
                                    VariantName = variantName,
                                    SubVariants = new List<string>()
                                };
                                product.Variants.Add(variant);
                            }

                            string subVariantName = reader["options"]?.ToString();
                            if (!string.IsNullOrWhiteSpace(subVariantName) &&
                                !variant.SubVariants.Contains(subVariantName))
                            {
                                variant.SubVariants.Add(subVariantName);
                            }
                        }
                    }
                }
            }

            return products;
        }







    }
}
