using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErpaPay.ClientExample.Models
{

    public class PaymentRequestModel
    {
        public string MerchantId { get; set; }
        public string Language { get; set; }
        public string TransactionId { get; set; }
        public string BackrefUrl { get; set; }
        public string Currency { get; set; }
        public string Installment { get; set; }
        public string Description { get; set; }
        public string BasketId { get; set; }
        public string PaymentChannel { get; set; }
        public double Amount { get; set; }
        public string CardNumber { get; set; }
        public string CardExpireMonth { get; set; }
        public string CardExpireYear { get; set; }
        public string CardSecurityCode { get; set; }
        public string CardOwner { get; set; }
        public string ClientIp { get; set; }
        public string TransactionTime { get; set; }
        public string Signature { get; set; }
    }

}
