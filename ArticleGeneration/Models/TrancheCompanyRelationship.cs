using System;
using System.Collections.Generic;

namespace ArticleGeneration.Models;

public partial class TrancheCompanyRelationship
{
    public int TrancheCompanyRelationshipId { get; set; }

    public int CompanyId { get; set; }

    public int? TrancheId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual Tranche? Tranche { get; set; }
}
