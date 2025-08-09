using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductInventorySystem.DomainModel
{
    public class HttpResponseModel
    {
        public Int64 ResponseCode { get; set; }
        public string? ResponseMessage { get; set; }
        public Int64 ResponseID { get; set; }
        public HttpResponseModel()
        {
            ResponseCode = 0;
            ResponseMessage = string.Empty;
            ResponseID = 0;
        }
    }
}
