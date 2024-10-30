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
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        private readonly TrancheRepository _trancheRepository;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
            _trancheRepository = new TrancheRepository(context);
        }

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
