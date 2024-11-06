using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGeneration.Repositories
{
    /// <summary>
    /// Defines methods for interacting with articles in the database.
    /// </summary>
    public interface IArticleRepository
    {
        /// <summary>
        /// Asynchronously adds a new article to the database.
        /// </summary>
        /// <param name="generatedText">The content of the article.</param>
        /// <param name="transactionId">The identifier of the transaction associated with this article.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddArticleAsync(string generatedText, int transactionId);
    }
}
