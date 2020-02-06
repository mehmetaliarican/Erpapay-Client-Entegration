using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErpaPay.ClientExample.Models
{

    public class PaymentResponseModel
    {
        public string Secure3dUrl { get; set; }
        public long SystemTime { get; set; }
        public bool Success { get; set; }
        public dynamic MessageCode { get; set; }
        public string Message { get; set; }
        public string UserMessage { get; set; }
    }

}
