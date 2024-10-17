using System;
using System.Collections.Generic;

namespace ArticleGeneration.Models;

public partial class Tranche
{
    public int TrancheId { get; set; }

    public string Name { get; set; } = null!;

    public string? Notes { get; set; }

    public string? AuthorNotes { get; set; }

    public decimal? Value { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal? Duration { get; set; }

    public int? TransactionId { get; set; }

    public int? TranchPrimaryTypeId { get; set; }

    public int? TranchSecondaryTypeId { get; set; }

    public int? TranchTertiaryTypeId { get; set; }

    public int? TrancheFeeId { get; set; }

    public int? TranchDebtServiceCoverRatioId { get; set; }

    public int? TranchLoanLifeCoverRatioId { get; set; }

    public decimal? Ltv { get; set; }

    public decimal? Spread { get; set; }

    public bool Multiple { get; set; }

    public decimal? RefSpread { get; set; }

    public string? Ratings { get; set; }

    public int? FitchId { get; set; }

    public int? MoodysId { get; set; }

    public int? Spid { get; set; }

    public int? RefBenchmarkId { get; set; }

    public int? KrollId { get; set; }

    public decimal? WeightedAverageLife { get; set; }

    public decimal? Balloon { get; set; }

    public decimal? BalloonValue { get; set; }

    public int? RateType { get; set; }

    public decimal? Coupon { get; set; }

    public string? Cusip { get; set; }

    public string? TrancheClass { get; set; }

    public bool IsActive { get; set; }

    public DateTime? LegalMaturityDate { get; set; }

    public virtual LoanReferenceRate? RefBenchmark { get; set; }

    public virtual ICollection<TrancheCompanyRelationship> TrancheCompanyRelationships { get; set; } = new List<TrancheCompanyRelationship>();

    public virtual Transaction? Transaction { get; set; }
}
