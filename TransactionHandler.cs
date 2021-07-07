using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderTaxProcessor
{
    public class TransactionHandler
    {
        HttpClient _httpClient;

        public TransactionHandler() => _httpClient = new HttpClient();

        public async Task<string> GetTodoItemDataAsync(int tokenId)
        {
            _httpClient.BaseAddress = new Uri("https://localhost:5001/");

            string uri = _httpClient.BaseAddress.ToString() + ((tokenId == 0) ? "api/TodoItems" : $"api/TodoItems/{tokenId}");

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequestMessage, default);

            if (httpResponse.IsSuccessStatusCode)
            {
                return await httpResponse.Content.ReadAsStringAsync();
            }
            else
            {
                return httpResponse.StatusCode.ToString();
            }
        }
    }
}