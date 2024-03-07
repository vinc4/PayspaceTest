using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Services
{
    public class CalculatorHttpService : ICalculatorHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly string dummyToken = "dummytoken123";

        public CalculatorHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7119/api/");
            _httpClient.DefaultRequestHeaders.Add("Dummy-Token", dummyToken);

        }

        public async Task<List<PostalCode>> GetPostalCodesAsync()
        {
            try
            {
               
                var response = await _httpClient.GetAsync("Calculator/posta1code"); 

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<PostalCode>>() ?? new List<PostalCode>();
                }
                else
                {
                    throw new Exception($"Cannot fetch postal codes, status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot fetch postal codes, status code: {ex.Message}");
            }
        }

        public async Task<List<CalculatorHistory>> GetHistoryAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("Calculator/history");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<CalculatorHistory>>() ?? new List<CalculatorHistory>();
                }
                else
                {
                    throw new Exception($"Cannot fetch Calculator History, error message: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot fetch Calculator History, error message: {ex.Message}");
            }
            
        }

        public async Task<CalculateResult> CalculateTaxAsync(CalculateRequest calculationRequest)
        {
            try
            {
                // Send a POST request to the "calculate-tax" endpoint with the CalculateRequest data
                var response = await _httpClient.PostAsJsonAsync("Calculator/calculateTax", calculationRequest);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response content to a CalculateResult object
                    return await response.Content.ReadFromJsonAsync<CalculateResult>();
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new ArgumentException($"{errorMessage}");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the request
                throw new Exception($"{ex.Message}");
            }

            return null;
        }
    }
}