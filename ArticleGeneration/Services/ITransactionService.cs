using ArticleGeneration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGeneration.Services
{
    public interface ITransactionService
    {
        string GeneratePrompt(Transaction transaction);
    }
}
