using System;
using System.Collections.Generic;

namespace ArticleGeneration.Models;

public partial class SubStructure
{
    public int SubStructureId { get; set; }

    public string SubStructureName { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
