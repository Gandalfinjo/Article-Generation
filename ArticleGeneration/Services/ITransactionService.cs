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
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task<List<Transaction>> GetNewOrUpdatedTransactionsAsync(DateTime lastChecked);
        string GeneratePrompt(Transaction transaction);
    }
}
