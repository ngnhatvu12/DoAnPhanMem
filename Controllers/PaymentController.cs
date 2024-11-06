using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Threading.Tasks;

namespace DoAnPhanMem.Controllers
{
    public class PaymentController : Controller
    {
        public PaymentController()
        {
            StripeConfiguration.ApiKey = "sk_test_51QHQ0IGVyUQsdJTU1qOVMdRrplEbGWsZC6fcZk9UTajsUkljyutoPhNd1uaDi8VksmDaxJc5N0F9t2j7Wp234exh00oc5Bwib3"; // Thay "YOUR_SECRET_KEY" bằng Secret Key của Stripe
        }

        [HttpPost("/Payment/CreateStripePayment")]
        public async Task<IActionResult> CreateStripePayment([FromBody] PaymentRequest request)
        {
            try
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = request.Amount, // số tiền tính theo cent
                    Currency = "vnd",
                    AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                    {
                        Enabled = true,
                    },
                };

                var service = new PaymentIntentService();
                var paymentIntent = await service.CreateAsync(options);

                return Json(new { clientSecret = paymentIntent.ClientSecret });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tạo PaymentIntent: " + ex.Message);
                return Json(new { error = "Lỗi khi tạo PaymentIntent" });
            }
        }
        public class PaymentRequest
        {
            public long Amount { get; set; }
        }

    }

}

