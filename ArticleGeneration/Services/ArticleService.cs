using ArticleGeneration.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGeneration.Services
{
    /// <summary>
    /// Provides the implementation for business logic related to articles.
    /// </summary>
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        /// <inheritdoc />
        /// <summary>
        /// Asynchronously adds a new article by delegating to the repository layer.
        /// </summary>
        /// <param name="generatedText">The content of the article.</param>
        /// <param name="transactionId">The identifier of the transaction associated with this article.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddArticleAsync(string generatedText, int transactionId)
        {
            await _articleRepository.AddArticleAsync(generatedText, transactionId);
        }
    }
}
