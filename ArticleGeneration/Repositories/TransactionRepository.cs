using ArticleGeneration.Data;
using ArticleGeneration.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGeneration.Repositories
{
    /// <summary>
    /// Provides the implementation for interacting with transactions in the database.
    /// </summary>
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        private readonly TrancheRepository _trancheRepository;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
            _trancheRepository = new TrancheRepository(context);
        }

        /// <inheritdoc />
        /// <summary>
        /// Asynchronously retrieves all transactions from the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result is a list of <see cref="Transaction"/> objects.</returns>
        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            var filteredTranches = await _trancheRepository.GetAllTranchesAsync();

            return await _context.Transactions
                .Where(t => t.Article == null)
                .Where(t => t.Value != null && t.Value > 0)
                .Include(t => t.Tranches)
                .Where(t => t.Tranches.Any(tr => filteredTranches.Contains(tr)))
                .ToListAsync();
        }

        /// <inheritdoc />
        /// <summary>
        /// Asynchronously retrieves new or updated transactions from the database.
        /// </summary>
        /// <param name="lastChecked">The date and time when the last check for new or updated transactions was made. The method will retrieve transactions that have been created or modified since this timestamp.</param>
        /// <returns>A task representing the asynchronous operation. The task result is a list of <see cref="Transaction"/> objects.</returns>
        public async Task<List<Transaction>> GetNewOrUpdatedTransactionsAsync(DateTime appStartTime)
        {
            var updatedTransactions = await _context.Transactions
                .Where(t => t.DateUpdated > appStartTime)
                .Include(t => t.Tranches)
                    .ThenInclude(tr => tr.TrancheCompanyRelationships)
                        .ThenInclude(tcr => tcr.Company)
                 .ToListAsync();

            var newTransactions = await _context.Transactions
                .Where(t => t.DateAdded > appStartTime)
                .Include(t => t.Tranches)
                    .ThenInclude (tr => tr.TrancheCompanyRelationships)
                        .ThenInclude(tcr => tcr.Company)
                .ToListAsync();

            var allTranasctions = updatedTransactions.Concat(newTransactions).ToList();

            return allTranasctions;
        }
    }
}
