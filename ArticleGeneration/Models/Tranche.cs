using System;
using System.Collections.Generic;

namespace ArticleGeneration.Models;

public partial class Tranche
{
    public int TrancheId { get; set; }

    public string Name { get; set; } = null!;

    public decimal? Value { get; set; }

    public int? TransactionId { get; set; }

    public bool Multiple { get; set; }

    public int? RefBenchmarkId { get; set; }

    public DateTime? LegalMaturityDate { get; set; }

    public virtual LoanReferenceRate? RefBenchmark { get; set; }

    public virtual ICollection<TrancheCompanyRelationship> TrancheCompanyRelationships { get; set; } = new List<TrancheCompanyRelationship>();

    public virtual Transaction? Transaction { get; set; }
}
