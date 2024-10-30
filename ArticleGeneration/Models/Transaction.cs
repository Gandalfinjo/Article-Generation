using System;
using System.Collections.Generic;

namespace ArticleGeneration.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Live { get; set; }

    public decimal? Value { get; set; }

    public bool? ValueIsEstimate { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateUpdated { get; set; }

    public int? TransactionTypeId { get; set; }

    public int? TransactionStageId { get; set; }

    public int? TransactionProcurementStageId { get; set; }

    public DateTime? PublishedOn { get; set; }

    public int? PublishedBy { get; set; }

    public DateTime? UnpublishedOn { get; set; }

    public int? UnpublishedBy { get; set; }

    public string? SelectedCurrency { get; set; }

    public int? TransactionInstrumentTypeId { get; set; }

    public int? ProductCategoryId { get; set; }

    public int? MarketTypeId { get; set; }

    public int? StructureId { get; set; }

    public int? SubStructureId { get; set; }

    public decimal? BankPricing { get; set; }

    public decimal? BankOfferPrice { get; set; }

    public int? BankBenchMarkId { get; set; }

    public int? ProductTypeId { get; set; }

    public virtual LoanReferenceRate? BankBenchMark { get; set; }

    public virtual MarketType? MarketType { get; set; }

    public virtual ProductCategory? ProductCategory { get; set; }

    public virtual ProductType? ProductType { get; set; }

    public virtual Structure? Structure { get; set; }

    public virtual SubStructure? SubStructure { get; set; }

    public virtual ICollection<Tranche> Tranches { get; set; } = new List<Tranche>();

    public virtual TransactionInstrumentType? TransactionInstrumentType { get; set; }

    public virtual TransactionProcurementStage? TransactionProcurementStage { get; set; }

    public virtual TransactionType? TransactionType { get; set; }

    public virtual Article? Article { get; set; }
}
