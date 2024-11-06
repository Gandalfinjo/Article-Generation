using ArticleGeneration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGeneration.Repositories
{
    /// <summary>
    /// Defines methods for interacting with transactions in the database.
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        /// Asynchronously retrieves all transactions from the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result is a list of <see cref="Transaction"/> objects.</returns>
        Task<List<Transaction>> GetAllTransactionsAsync();

        /// <summary>
        /// Asynchronously retrieves new or updated transactions from the database.
        /// </summary>
        /// <param name="lastChecked">The date and time when the last check for new or updated transactions was made. The method will retrieve transactions that have been created or modified since this timestamp.</param>
        /// <returns>A task representing the asynchronous operation. The task result is a list of <see cref="Transaction"/> objects.</returns>
        Task<List<Transaction>> GetNewOrUpdatedTransactionsAsync(DateTime lastChecked);
    }
}
