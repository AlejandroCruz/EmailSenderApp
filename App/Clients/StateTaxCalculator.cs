using EmailSenderApp.App.DTOs;
using EmailSenderApp.Domain.DataEntities;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailSenderApp.App.Clients
{
    internal interface IStateTaxCalculator
    {
    }

    class StateTaxCalculator : IStateTaxCalculator
    {
        private readonly HttpClient _httpClient;
        private IRequestDto _requestDto;
        private ResponseDto _responseDto;

        public StateTaxCalculator(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _requestDto = new RequestDto();
            _responseDto = new ResponseDto();
        }

        public async Task<Order> StateTaxCalculateAsync(Order pendingOrder, CancellationToken cancellationToken)
        {
            try
            {
                MapToDto(pendingOrder);

                Uri uri = new UriBuilder("https://localhost:44306/api/StateTax/addtax").Uri;
                HttpRequestMessage requestMessage = BuildHttpRequest(_requestDto, HttpMethod.Post, uri);
                _responseDto = await SendRequestAsync<ResponseDto>(requestMessage, cancellationToken);

                if (_responseDto == default)
                {
                    Log.Information($"Request API error: {_responseDto.TransactionId}.");
                    return MapToOrder(new ResponseDto { TransactionId = _responseDto.TransactionId }, pendingOrder, "API request error.");
                }

                Console.WriteLine(Environment.NewLine);
                Log.Information($"Order: {_requestDto.TransactionId}, API request success.");

                Order order = MapToOrder(_responseDto, pendingOrder);

                return order;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw;
            }
        }

        private Order MapToOrder(ResponseDto responseDto, Order pendingOrder, string message = null)
        {
            //pendingOrder.Error = true;
            //pendingOrder.TransMessage = message ?? responseDto.Message;
            pendingOrder.DateModified = DateTime.UtcNow;
            pendingOrder.PayTransNumber = responseDto.TransactionId;
            pendingOrder.OrderAmount = responseDto.Amount;

            return pendingOrder;
        }

        private async Task<T> SendRequestAsync<T>(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage httpResponse = await _httpClient.SendAsync(requestMessage, cancellationToken);

                if (httpResponse.IsSuccessStatusCode)
                {
                    // --> DEBUG
                    var tmp = await httpResponse.Content.ReadAsStringAsync();
                    var tmp1 = JsonConvert.DeserializeObject<T>(tmp);

                    return tmp1;
                    // DEBUG <--
                }
                else
                {
                    return default;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw;
            }
        }

        private HttpRequestMessage BuildHttpRequest<T>(T requestDto, HttpMethod httpMethod, Uri uri)
        {
            string headerType = new MediaTypeHeaderValue("application/json").MediaType;
            string jsonObj = JsonConvert.SerializeObject(requestDto);

            HttpRequestMessage message = new HttpRequestMessage(httpMethod, uri)
            {
                Content = new StringContent(jsonObj, Encoding.UTF8, headerType)
            };

            return message;
        }

        private void MapToDto(Order pendingOrder)
        {
            _requestDto.Amount = pendingOrder.OrderAmount;
            _requestDto.StateCode = pendingOrder.StateCode;
            _requestDto.TransactionId = pendingOrder.PayTransNumber;
        }
    }
}
