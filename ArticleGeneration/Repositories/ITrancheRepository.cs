using ArticleGeneration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGeneration.Repositories
{
    public interface ITrancheRepository
    {
        Task<List<Tranche>> GetAllTranchesAsync();
    }
}
