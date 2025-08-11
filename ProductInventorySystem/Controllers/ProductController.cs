using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProductInventorySystem.DomainModel;
using ProductInventorySystem.Interface;

namespace ProductInventorySystem.Controllers
{
    [ApiController]
    [Route("api/[action]")]

    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IConfiguration configuration, IProductService productService)
        {
            _logger = logger;
            _configuration = configuration;
            _productService = productService;
            _productService.connectionString = _configuration["ConnectionStrings"];
        }
        [HttpGet]
        [ActionName("product")]
        [EnableCors()]
        [ApiExplorerSettings(IgnoreApi = false)]
        //[Authorize]
        public IActionResult GetProducts([FromQuery] FilterModel filter)
        {
           
            List<ProductModel> products = new List<ProductModel>();
            products = _productService.GetProductsAndStocks(filter);
            return Ok(products);
        }
        [HttpPost]
        [ActionName("product")]
        [EnableCors()]
        [ApiExplorerSettings(IgnoreApi = false)]
        [Authorize]
        public IActionResult UpdateProduct(ProductJson product)
        {
            HttpResponseModel httpResponseModel = new HttpResponseModel();

            httpResponseModel = _productService.AddProduct(product);
            return Ok(httpResponseModel);
        }
        [HttpPost]
        [ActionName("stock")]
        [EnableCors()]
        [ApiExplorerSettings(IgnoreApi = false)]
        //[Authorize]
        public IActionResult UpdateStock(ProductJson product)
        {
            HttpResponseModel httpResponseModel = new HttpResponseModel();

            httpResponseModel = _productService.AddStock(product);
            return Ok(httpResponseModel);
        }


        //[HttpDelete]
        //[ActionName("product")]
        //public IActionResult Delete()
        //{
        //    return Ok('g');
        //}

    }
}
