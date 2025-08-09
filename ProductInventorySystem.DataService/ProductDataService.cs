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



    }
}
