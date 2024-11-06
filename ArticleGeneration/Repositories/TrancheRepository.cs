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
    /// Provides the implementation for interacting with tranches in the database.
    /// </summary>
    public class TrancheRepository : ITrancheRepository
    {
        private readonly AppDbContext _context;

        public TrancheRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        /// <summary>
        /// Asynchronously retrieves all tranches from the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result is a list of <see cref="Tranche"/> objects.</returns>
        public async Task<List<Tranche>> GetAllTranchesAsync()
        {
            return await _context.Tranches
                .Where(t => t.TrancheCompanyRelationships.Count() > 0)
                .Include(t => t.TrancheCompanyRelationships)
                    .ThenInclude(tcr => tcr.Company)
                .ToListAsync();
        }
    }
}
