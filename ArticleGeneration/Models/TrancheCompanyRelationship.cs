using System;
using System.Collections.Generic;

namespace ArticleGeneration.Models;

public partial class TrancheCompanyRelationship
{
    public int TrancheCompanyRelationshipId { get; set; }

    public string? Notes { get; set; }

    public decimal? LeagueTableCreditValue { get; set; }

    public decimal? Percentage { get; set; }

    public decimal? EquityValue { get; set; }

    public decimal? UnderwrittenValue { get; set; }

    public int CompanyId { get; set; }

    public int? TranchCompanyRelationshipRoleId { get; set; }

    public int? TranchCompanyRelationshipSubRoleId { get; set; }

    public int LeagueTableCreditId { get; set; }

    public int? TrancheId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual Company LeagueTableCredit { get; set; } = null!;

    public virtual Tranche? Tranche { get; set; }
}
