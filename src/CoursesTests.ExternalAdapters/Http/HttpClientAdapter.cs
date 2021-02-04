using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoursesTests.ExternalAdapters.Http
{
    public class HttpClientAdapter : IHttpClientAdapter
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientAdapter(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task PostAsync<T>(string clientName, string endpointSufix, T dto)
        {
            var httpClient = _httpClientFactory.CreateClient(clientName);

            using (var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json"))
            {
                var response = await httpClient.PostAsync(endpointSufix, content);
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
