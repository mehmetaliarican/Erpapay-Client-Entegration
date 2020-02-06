using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using ErpaPay.ClientExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace ErpaPay.ClientExample.Controllers
{
    public class PaymentController : Controller
    {
        private string paymemntUrl = "https://testapi.erpara.com.tr/api/payment3d/secure3D/v1";
        private string passwordUrl = "https://testapi.erpara.com.tr/api/payment3d/get3dSign/v1";
        private string completeUrl = "https://testapi.erpara.com.tr/api/payment3d/complete3dpayment/v1";
        private string MerchantId = "5e394f204079b9deffdb9d13";
        private string SecretKey = "FvJSS4Q-rrDT6oyGxpBz";

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("odeme_yap")]
        public async Task<IActionResult> DoPayment()
        {
            using (HttpClient client = new HttpClient())
            {
                var request = new PaymentRequestModel
                {
                    Amount = Convert.ToDecimal("12.5",new CultureInfo("tr-TR")),
                    BackrefUrl = "http://localhost:57837/getresponse",
                    BasketId = "123456",
                    CardExpireMonth = "12",
                    CardExpireYear = "30",
                    CardNumber = "4508034508034509",
                    CardSecurityCode = "000",
                    CardOwner = "testname",
                    ClientIp = "10.10.2.27",
                    Description = "May the force be with you",
                    Currency = "TRY",
                    Installment = "0",
                    Language = "TR",
                    MerchantId = this.MerchantId,
                    PaymentChannel = "Api",
                    TransactionId = Guid.NewGuid().ToString(),
                    TransactionTime = ((Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString()
                };
                request.Signature = SHA512_ComputeHash(this.SecretKey + this.MerchantId + request.TransactionId + request.TransactionTime + request.Amount + request.Currency + request.Installment + request.CardNumber, this.SecretKey);

                var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(paymemntUrl, jsonContent);
                var content = await response.Content.ReadAsStringAsync();
                var responseObj = JsonSerializer.Deserialize<PaymentResponseModel>(content);
                return View(responseObj);
            }
        }
        [Route("getresponse")]
        public async Task<IActionResult> GetResponse(PasswordResponseModel responseModel)
        {
            using (HttpClient client = new HttpClient())
            {
                var completeRequest = new CompleteRequestModel
                {
                    MerchantId = this.MerchantId,
                    TransactionId = responseModel.TransactionId
                };

                var content = new StringContent(JsonSerializer.Serialize(completeRequest), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(completeUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseObj = JsonSerializer.Deserialize<CompleteResponseModel>(responseContent);
                return View("PaymentResult",responseObj);
            }
        }



        public static string SHA512_ComputeHash(string text, string secretKey)
        {
            var hash = new StringBuilder(); ;
            byte[] secretkeyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] inputBytes = Encoding.UTF8.GetBytes(text);
            using (var hmac = new HMACSHA512(secretkeyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }
            return hash.ToString();
        }
    }
}