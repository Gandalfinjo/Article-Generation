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
    public class TrancheRepository : ITrancheRepository
    {
        private readonly AppDbContext _context;

        public TrancheRepository(AppDbContext context)
        {
            _context = context;
        }

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
