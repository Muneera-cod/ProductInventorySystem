using ProductInventorySystem.DataService;
using ProductInventorySystem.DomainModel;
using ProductInventorySystem.Interface;

namespace ProductInventorySystem.Service
{
    public class ProductService : IProductService
    {
        public string connectionString { get; set;}
        public HttpResponseModel AddProduct(ProductJson productJson)
        {
             ProductDataService productDataService = new ProductDataService(connectionString);
             return productDataService.AddProduct(productJson);
        }
        public HttpResponseModel AddStock(ProductJson productJson)
        {
            ProductDataService productDataService = new ProductDataService(connectionString);
            return productDataService.AddStock(productJson);
        }
        public List<ProductModel> GetProductsAndStocks(FilterModel filter)
        {
            ProductDataService productDataService = new ProductDataService(connectionString);
            return productDataService.SelectProduct(filter);
        }
        

    }
}
