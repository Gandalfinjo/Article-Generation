using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGeneration.Services
{
    /// <summary>
    /// Defines methods for interacting with the OpenAI API to generate content based on prompts.
    /// </summary>
    public interface IOpenAIService
    {
        /// <summary>
        /// Asynchronously generates an article based on the provided prompt string, for use with the OpenAI API.
        /// </summary>
        /// <param name="prompt">The prompt string that was generated using transaction data, which is sent to the OpenAI API to generate the article.</param>
        /// <returns>A task representing the asynchronous operation. The task result is a string representing the generated article content.</returns>
        Task<string> GenerateArticleAsync(string prompt);
    }
}
