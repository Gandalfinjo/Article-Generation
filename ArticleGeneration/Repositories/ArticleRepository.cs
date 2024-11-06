using ArticleGeneration.Data;
using ArticleGeneration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGeneration.Repositories
{
    /// <summary>
    /// Provides the implementation for interacting with articles in the database.
    /// </summary>
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _context;

        public ArticleRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        /// <summary>
        /// Asynchronously adds a new article to the database.
        /// </summary>
        /// <param name="generatedText">The content of the article.</param>
        /// <param name="transactionId">The identifier of the transaction associated with this article.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddArticleAsync(string generatedText, int transactionId)
        {
            var article = new Article
            {
                Title = null,
                ShortDescription = null,
                FullDescription = generatedText,
                TransactionId = transactionId
            };

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
        }
    }
}
