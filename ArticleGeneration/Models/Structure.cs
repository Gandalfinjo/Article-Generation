using System;
using System.Collections.Generic;

namespace ArticleGeneration.Models;

public partial class Structure
{
    public int StructureId { get; set; }

    public string StructureName { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
