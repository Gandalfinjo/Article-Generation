using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGeneration.Repositories
{
    public interface IArticleRepository
    {
        Task AddArticleAsync(string generatedText, int transactionId);
    }
}
