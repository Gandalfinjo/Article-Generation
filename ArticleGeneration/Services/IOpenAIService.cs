using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGeneration.Services
{
    public interface IOpenAIService
    {
        Task<string> GenerateArticleAsync(string prompt);
    }
}
