using System;
using System.Collections.Generic;

namespace ArticleGeneration.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public string Name { get; set; } = null!;

    public string? AlternateName { get; set; }

    public string? Description { get; set; }

    public string? AuthorNotes { get; set; }

    public bool HideFromLeagueTables { get; set; }

    public bool Live { get; set; }

    public string? Spv { get; set; }

    public bool? Ppp { get; set; }

    public decimal? Value { get; set; }

    public bool? ValueIsEstimate { get; set; }

    public decimal? Debt { get; set; }

    public decimal? Equity { get; set; }

    public string? DebtEquityRatio { get; set; }

    public string? ConcessionNotes { get; set; }

    public DateTime? ConcessionStartDate { get; set; }

    public DateTime? ConcessionEndDate { get; set; }

    public decimal? ConcessionDuration { get; set; }

    public string? CashflowNotes { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateUpdated { get; set; }

    public int? TransactionTypeId { get; set; }

    public int? TransactionFinanceTypeId { get; set; }

    public int? TransactionStageId { get; set; }

    public int? TransactionConcessionTypeId { get; set; }

    public int? TransactionProcurementStageId { get; set; }

    public int? CashflowModelId { get; set; }

    public int? LastUpdatedByUserId { get; set; }

    public int Status { get; set; }

    public int CreatedBy { get; set; }

    public int? RejectedBy { get; set; }

    public DateTime? RejectedOn { get; set; }

    public string? RejectionReasons { get; set; }

    public int? Assignee { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? DraftedOn { get; set; }

    public int? DraftedBy { get; set; }

    public DateTime? SubmittedOn { get; set; }

    public int? SubmittedBy { get; set; }

    public DateTime? PublishedOn { get; set; }

    public int? PublishedBy { get; set; }

    public DateTime? UnpublishedOn { get; set; }

    public int? UnpublishedBy { get; set; }

    public string? SelectedCurrency { get; set; }

    public bool? Stalled { get; set; }

    public bool? Rbl { get; set; }

    public int? ProjectId { get; set; }

    public decimal? LeaseTerm { get; set; }

    public int? TransactionInstrumentTypeId { get; set; }

    public decimal? Spread { get; set; }

    public int? ProductCategoryId { get; set; }

    public int? MarketTypeId { get; set; }

    public int? StructureId { get; set; }

    public int? SubStructureId { get; set; }

    public string? RfpconsideredStructures { get; set; }

    public bool Rfpclosed { get; set; }

    public decimal? BankPricing { get; set; }

    public decimal? BankOfferPrice { get; set; }

    public int? BankListingId { get; set; }

    public int? BankBenchMarkId { get; set; }

    public int? ProductTypeId { get; set; }

    public int? ProjectCreationUserId { get; set; }

    public bool? IsFeatured { get; set; }

    public DateTime? LeaseStartDate { get; set; }

    public DateTime? LeaseEndDate { get; set; }

    public virtual LoanReferenceRate? BankBenchMark { get; set; }

    public virtual MarketType? MarketType { get; set; }

    public virtual ProductCategory? ProductCategory { get; set; }

    public virtual ProductType? ProductType { get; set; }

    public virtual Structure? Structure { get; set; }

    public virtual SubStructure? SubStructure { get; set; }

    public virtual TransactionProcurementStage? TransactionProcurementStage { get; set; }

    public virtual TransactionType? TransactionType { get; set; }
}
