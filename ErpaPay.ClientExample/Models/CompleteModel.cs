using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErpaPay.ClientExample.Models
{
    public class CompleteRequestModel
    {
        public string MerchantId { get; set; }

        public string TransactionId { get; set; }

    }
    public class CompleteResponseModel
    {
        public long SystemTime { get; set; }
        public bool Success { get; set; }
        public string MessageCode { get; set; }
        public string UserMessage { get; set; }
        public string Message { get; set; }
    }
}
