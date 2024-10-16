using System;
using System.Collections.Generic;

namespace ArticleGeneration.Models;

public partial class TransactionProcurementStage
{
    public int TransactionProcurementStageId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
