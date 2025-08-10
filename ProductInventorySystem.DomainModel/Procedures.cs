using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductInventorySystem.DomainModel
{
    public class Procedures
    {
        public string SP_Product_InsertProduct = "usp_Product_InsertProduct";
        public string SP_Product_InsertStock = "usp_Stock_InsertStock";
        public string SP_Product_GetProductsAndStock = "usp_Product_GetByStockOrProduct";
    }
}
