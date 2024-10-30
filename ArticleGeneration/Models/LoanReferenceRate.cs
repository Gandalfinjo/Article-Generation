using System;
using System.Collections.Generic;

namespace ArticleGeneration.Models;

public partial class LoanReferenceRate
{
    public int LoanReferenceRateId { get; set; }

    public string Name { get; set; } = null!;

    public string? City { get; set; }

    public virtual ICollection<Tranche> Tranches { get; set; } = new List<Tranche>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
