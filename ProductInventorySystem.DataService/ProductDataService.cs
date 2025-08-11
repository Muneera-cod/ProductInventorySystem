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
            List<ProductModel> products = new List<ProductModel>();
            Procedures procedures = new Procedures();

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
                        ProductModel product = new ProductModel();

                        int productIdIndex = reader.GetOrdinal("product_id");
                        if (!reader.IsDBNull(productIdIndex))
                        {
                            product.ProductId = reader.GetGuid(productIdIndex);
                        }
                        else
                        {
                            product.ProductId = Guid.Empty;
                        }

                        product.ProductName = reader["name"]?.ToString() ?? string.Empty;
                        product.Variants = new List<Variant>();

                        string variantName = reader["variant"]?.ToString();
                        if (!string.IsNullOrWhiteSpace(variantName))
                        {
                            var variant = new Variant
                            {
                                VariantName = variantName,
                                SubVariants = new List<string>()
                            };

                            string subVariantName = reader["options"]?.ToString();
                            if (!string.IsNullOrWhiteSpace(subVariantName))
                            {
                                variant.SubVariants.Add(subVariantName);
                            }

                            product.Variants.Add(variant);
                        }

                        products.Add(product); 
                    }

                }
            }

            return products;
        }






    }
}
