using EasyShopConnect_.Web.Models.DTOs;
using EasyShopConnect_.Web.Services.Interface;
using Newtonsoft.Json; // Importing Newtonsoft.Json for JSON serialization/deserialization
using System.Net; // Importing System.Net for HttpStatusCode
using System.Text; // Importing System.Text for StringContent
using static EasyShopConnect_.Web.Models.Utility.StaticDetails; // Importing StaticDetails for ApiType enum

namespace EasyShopConnect_.Web.Services
{
    // This class implements the IBaseService interface
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory; // Dependency injection for HttpClientFactory

        // Constructor to inject IHttpClientFactory dependency
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Method to send asynchronous HTTP requests
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try
            {
                // Creating an instance of HttpClient using IHttpClientFactory
                HttpClient client = _httpClientFactory.CreateClient("EasyShopConnectAPI");

                // Creating an instance of HttpRequestMessage
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");

                // Setting the request URI
                message.RequestUri = new Uri(requestDto.Url);

                // Checking if request data is present
                if (requestDto.Data != null)
                {
                    // Serializing request data to JSON and adding it as content to the request message
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data),
                        Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? apiResponse = null;

                // Switching based on the type of HTTP request (GET, POST, DELETE, PUT)
                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                // Sending the HTTP request asynchronously and awaiting the response
                apiResponse = await client.SendAsync(message);

                // Handling response based on status code
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        // Returning a ResponseDto indicating unauthorized access
                        return new ResponseDto
                        {
                            IsSuccess = false,
                            Message = "Unauthorized"
                        };
                    case HttpStatusCode.InternalServerError:
                        // Returning a ResponseDto indicating internal server error
                        return new ResponseDto
                        {
                            IsSuccess = false,
                            Message = "InternalServerError"
                        };
                    default:
                        // Reading content from the response and deserializing it to ResponseDto
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                // Handling exceptions and returning a ResponseDto with error message
                var dto = new ResponseDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return dto;
            }
        }
    }
}
