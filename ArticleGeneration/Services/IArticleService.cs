using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGeneration.Services
{
    /// <summary>
    /// Provides methods for business logic related to article management.
    /// </summary>
    public interface IArticleService
    {
        /// <summary>
        /// Asynchronously adds a new article by delegating to the repository layer.
        /// </summary>
        /// <param name="generatedText">The content of the article.</param>
        /// <param name="transactionId">The identifier of the transaction associated with this article.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddArticleAsync(string generatedText, int transactionId);
    }
}
