using System;
using System.Collections.Generic;

namespace ArticleGeneration.Models;

public partial class Company
{
    public int CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? TurnOverInMillionsOfDollars { get; set; }

    public string? WebAddress { get; set; }

    public string? EmailAddress { get; set; }

    public string? AuthorNotes { get; set; }

    public bool? EquatorPrinciplesCompliant { get; set; }

    public string? RssLink { get; set; }

    public string? LinkedIn { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime DateUpdated { get; set; }

    public string? Twitter { get; set; }

    public string? YouTube { get; set; }

    public string? TelephoneNumber { get; set; }

    public bool? DisplayOnCompanyHub { get; set; }

    public int? YearFounded { get; set; }

    public int? LeagueTableAccreditationCompanyId { get; set; }

    public int CompanyTypeId { get; set; }

    public int? EmployeeRangeId { get; set; }

    public int? CompanyOwnershipTypeId { get; set; }

    public int? CompanyStatusId { get; set; }

    public int? FileId { get; set; }

    public int? PrimaryAddressId { get; set; }

    public int? CompanyTypeNew { get; set; }

    public bool? ForceCompanyReportPage { get; set; }

    public bool HideFromLeagueTables { get; set; }

    public string? IcaoCode { get; set; }

    public virtual CompanyType CompanyType { get; set; } = null!;

    public virtual ICollection<Company> InverseLeagueTableAccreditationCompany { get; set; } = new List<Company>();

    public virtual Company? LeagueTableAccreditationCompany { get; set; }

    public virtual ICollection<TrancheCompanyRelationship> TrancheCompanyRelationshipCompanies { get; set; } = new List<TrancheCompanyRelationship>();

    public virtual ICollection<TrancheCompanyRelationship> TrancheCompanyRelationshipLeagueTableCredits { get; set; } = new List<TrancheCompanyRelationship>();
}
