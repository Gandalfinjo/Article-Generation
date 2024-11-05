using ArticleGeneration.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGeneration.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task AddArticleAsync(string generatedText, int transactionId)
        {
            await _articleRepository.AddArticleAsync(generatedText, transactionId);
        }
    }
}
