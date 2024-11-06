using ArticleGeneration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArticleGeneration.Services
{
    /// <summary>
    /// Provides the implementation for interacting with the OpenAI API to generate content based on prompts.
    /// </summary>
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

        /// <inheritdoc />
        /// <summary>
        /// Asynchronously generates an article based on the provided prompt string, for use with the OpenAI API.
        /// </summary>
        /// <param name="prompt">The prompt string that was generated using transaction data, which is sent to the OpenAI API to generate the article.</param>
        /// <returns>A task representing the asynchronous operation. The task result is a string representing the generated article content.</returns>
        public async Task<string> GenerateArticleAsync(string prompt)
        {
            var requestBody = new
            {
                prompt = prompt,
                max_tokens = 750
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiUrl, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            var openAiResponse = JsonSerializer.Deserialize<OpenAIResponse>(responseContent);

            return openAiResponse?.choices?.FirstOrDefault()?.text ?? "No text generated";
        }
    }
}
