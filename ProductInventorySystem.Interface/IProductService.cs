using ProductInventorySystem.DomainModel;

namespace ProductInventorySystem.Interface
{
     public interface IProductService
    {
        public string connectionString { get; set; }

        HttpResponseModel AddProduct(ProductJson productJson);

        HttpResponseModel AddStock(ProductJson productJson);
        List<ProductModel> GetProductsAndStocks(FilterModel filter);

    }
}
