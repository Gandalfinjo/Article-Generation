using System;
using System.Collections.Generic;

namespace ArticleGeneration.Models;

public partial class Company
{
    public int CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public int CompanyTypeId { get; set; }

    public virtual CompanyType CompanyType { get; set; } = null!;

    public virtual ICollection<TrancheCompanyRelationship> TrancheCompanyRelationshipCompanies { get; set; } = new List<TrancheCompanyRelationship>();
}
