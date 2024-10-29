using ArticleGeneration.Data;
using ArticleGeneration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGeneration.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _context;

        public ArticleRepository(AppDbContext context)
        {
            _context = context;
        }

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
