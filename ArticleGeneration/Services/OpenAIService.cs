using ArticleGeneration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArticleGeneration.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        private readonly string _apiKey;

        public OpenAIService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["OpenAI:ApiUrl"]!;
            _apiKey = configuration["OpenAI:ApiKey"]!;

            _httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);
        }

        public async Task<string> GenerateArticleAsync(string prompt)
        {
            var requestBody = new
            {
                prompt = prompt,
                max_tokens = 1000
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiUrl, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            var openAiResponse = JsonSerializer.Deserialize<OpenAIResponse>(responseContent);

            return openAiResponse?.choices?.FirstOrDefault()?.text ?? "No text generated";

            // return responseContent;
        }
    }
}
