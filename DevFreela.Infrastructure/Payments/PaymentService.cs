using DevFreela.Core.DTOs;
using DevFreela.Core.Payments;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastructure.Payments
{
    public class PaymentService : IPaymentService
    {
        //private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMessageBusService _messageBusService;
        private const string QUEUE_NAME = "Payments";
        public PaymentService(IMessageBusService messageBusService)
        {
            _messageBusService = messageBusService;
        }

        public void ProcessPayment(PaymentInfoDTO paymentInfoDTO)
        {
            //SEM FILA
            //var url = $"{_paymentsBaseUrl}/api/payments";
            //var paymentInfoJson = new StringContent(
            //    JsonSerializer.Serialize(paymentInfoDTO), 
            //    Encoding.UTF8, 
            //    "application/json"
            //    );

            //var httpClient = _httpClientFactory.CreateClient("Payments");

            //var response = await httpClient.PostAsync(url, paymentInfoJson);

            //return response.IsSuccessStatusCode;

            //COM FILA

            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);

            var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);

            _messageBusService.Publish(QUEUE_NAME, paymentInfoBytes);
        }
    }
}
