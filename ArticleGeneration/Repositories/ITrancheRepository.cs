using ArticleGeneration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGeneration.Repositories
{
    /// <summary>
    /// Defines methods for interacting with tranches in the database.
    /// </summary>
    public interface ITrancheRepository
    {
        /// <summary>
        /// Asynchronously retrieves all tranches from the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result is a list of <see cref="Tranche"/> objects.</returns>
        Task<List<Tranche>> GetAllTranchesAsync();
    }
}
