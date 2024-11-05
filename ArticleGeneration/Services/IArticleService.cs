using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGeneration.Services
{
    public interface IArticleService
    {
        Task AddArticleAsync(string generatedText, int transactionId);
    }
}
