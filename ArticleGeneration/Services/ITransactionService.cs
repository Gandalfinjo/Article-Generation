using ArticleGeneration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGeneration.Services
{
    /// <summary>
    /// Provides methods for business logic related to transaction management.
    /// </summary>
    public interface ITransactionService
    {
        /// <summary>
        /// Asynchronously retrieves all transactions from the database by delegating to the repository layer.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result is a list of <see cref="Transaction"/> objects.</returns>
        Task<List<Transaction>> GetAllTransactionsAsync();

        /// <summary>
        /// Asynchronously retrieves new or updated transactions from the database by delegating to the repository layer.
        /// </summary>
        /// <param name="lastChecked">The date and time when the last check for new or updated transactions was made. The method will retrieve transactions that have been created or modified since this timestamp.</param>
        /// <returns>A task representing the asynchronous operation. The task result is a list of <see cref="Transaction"/> objects.</returns>
        Task<List<Transaction>> GetNewOrUpdatedTransactionsAsync(DateTime lastChecked);

        /// <summary>
        /// Generates a prompt string based on the provided transaction data, to be used in an API request.
        /// </summary>
        /// <param name="transaction">The transaction object containing the data used to generate the prompt.</param>
        /// <returns>A string representing the generated prompt that will be sent to the OpenAI API.</returns>
        string GeneratePrompt(Transaction transaction);
    }
}
