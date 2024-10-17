using System;
using System.Collections.Generic;
using ArticleGeneration.Models;
using Microsoft.EntityFrameworkCore;

namespace ArticleGeneration.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyType> CompanyTypes { get; set; }

    public virtual DbSet<LoanReferenceRate> LoanReferenceRates { get; set; }

    public virtual DbSet<MarketType> MarketTypes { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<Structure> Structures { get; set; }

    public virtual DbSet<SubStructure> SubStructures { get; set; }

    public virtual DbSet<Tranche> Tranches { get; set; }

    public virtual DbSet<TrancheCompanyRelationship> TrancheCompanyRelationships { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TransactionInstrumentType> TransactionInstrumentTypes { get; set; }

    public virtual DbSet<TransactionProcurementStage> TransactionProcurementStages { get; set; }

    public virtual DbSet<TransactionType> TransactionTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Companie__2D971CAC7A521F79");

            entity.HasIndex(e => e.CompanyOwnershipTypeId, "idx_CompanyCompanyOwnershipTypeId").HasFillFactor(80);

            entity.HasIndex(e => e.CompanyStatusId, "idx_CompanyCompanyStatus").HasFillFactor(80);

            entity.HasIndex(e => e.CompanyTypeId, "idx_CompanyCompanyType").HasFillFactor(80);

            entity.HasIndex(e => e.EmployeeRangeId, "idx_CompanyEmployeeRange").HasFillFactor(80);

            entity.HasIndex(e => e.LeagueTableAccreditationCompanyId, "idx_CompanyLeagueTableAccreditationCompany").HasFillFactor(80);

            entity.HasIndex(e => e.FileId, "idx_CompanyLogo").HasFillFactor(80);

            entity.HasIndex(e => e.Name, "idx_CompanyName").HasFillFactor(80);

            entity.HasIndex(e => e.PrimaryAddressId, "idx_CompanyPrimaryAddress").HasFillFactor(80);

            entity.Property(e => e.DateAdded).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.EmailAddress).HasMaxLength(255);
            entity.Property(e => e.ForceCompanyReportPage).HasDefaultValue(false);
            entity.Property(e => e.IcaoCode).HasMaxLength(10);
            entity.Property(e => e.LinkedIn).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.RssLink).HasMaxLength(500);
            entity.Property(e => e.TelephoneNumber).HasMaxLength(40);
            entity.Property(e => e.TurnOverInMillionsOfDollars).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Twitter).HasMaxLength(500);
            entity.Property(e => e.WebAddress).HasMaxLength(500);
            entity.Property(e => e.YouTube).HasMaxLength(500);

            entity.HasOne(d => d.CompanyType).WithMany(p => p.Companies)
                .HasForeignKey(d => d.CompanyTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK2C073D856669B19A");

            entity.HasOne(d => d.LeagueTableAccreditationCompany).WithMany(p => p.InverseLeagueTableAccreditationCompany)
                .HasForeignKey(d => d.LeagueTableAccreditationCompanyId)
                .HasConstraintName("FK2C073D85603871C3");
        });

        modelBuilder.Entity<CompanyType>(entity =>
        {
            entity.HasKey(e => e.CompanyTypeId).HasName("PK__CompanyTypes__2DB35E9C");

            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<LoanReferenceRate>(entity =>
        {
            entity.HasKey(e => e.LoanReferenceRateId).HasName("PK__LoanRefe__0FCE9E069FFC800D");

            entity.HasIndex(e => e.SecondaryRegionId, "idx_LoanReferenceRateSecondaryRegion").HasFillFactor(80);

            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Tooltip).HasMaxLength(255);
        });

        modelBuilder.Entity<MarketType>(entity =>
        {
            entity.ToTable("MarketType");

            entity.HasIndex(e => e.MarketTypeName, "UQ__MarketTy__3654D6C05A830D46").IsUnique();

            entity.Property(e => e.MarketTypeId).HasColumnName("MarketTypeID");
            entity.Property(e => e.MarketTypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryId).HasName("PK_TransactionCategory");

            entity.ToTable("ProductCategory");

            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.ProductTypeId).HasName("PK_ProductType");

            entity.Property(e => e.ProductTypeId).HasColumnName("ProductTypeID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Structure>(entity =>
        {
            entity.ToTable("Structure");

            entity.Property(e => e.StructureId).HasColumnName("StructureID");
            entity.Property(e => e.StructureName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SubStructure>(entity =>
        {
            entity.ToTable("SubStructure");

            entity.Property(e => e.SubStructureId).HasColumnName("SubStructureID");
            entity.Property(e => e.SubStructureName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tranche>(entity =>
        {
            entity.HasKey(e => e.TrancheId).HasName("PK__Tranches__EED47AC6F6188C7C");

            entity.HasIndex(e => e.TranchDebtServiceCoverRatioId, "idx_TranchDebtServiceCoverRatioId").HasFillFactor(80);

            entity.HasIndex(e => e.TranchLoanLifeCoverRatioId, "idx_TranchLoanLifeCoverRatioId").HasFillFactor(80);

            entity.HasIndex(e => e.TrancheFeeId, "idx_TrancheFeeId").HasFillFactor(80);

            entity.HasIndex(e => e.TranchPrimaryTypeId, "idx_TranchePrimaryType").HasFillFactor(80);

            entity.HasIndex(e => e.TranchSecondaryTypeId, "idx_TrancheSecondaryType").HasFillFactor(80);

            entity.HasIndex(e => e.TranchTertiaryTypeId, "idx_TrancheTertiaryType").HasFillFactor(80);

            entity.HasIndex(e => e.TransactionId, "idx_TrancheTransaction").HasFillFactor(80);

            entity.Property(e => e.AuthorNotes).HasMaxLength(2000);
            entity.Property(e => e.Balloon).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.BalloonValue).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Coupon).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Cusip).HasMaxLength(30);
            entity.Property(e => e.Duration).HasColumnType("decimal(19, 5)");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.LegalMaturityDate).HasColumnType("datetime");
            entity.Property(e => e.Ltv)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("LTV");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Notes).HasMaxLength(2000);
            entity.Property(e => e.Ratings).HasMaxLength(18);
            entity.Property(e => e.RefBenchmarkId).HasColumnName("RefBenchmarkID");
            entity.Property(e => e.RefSpread).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Spid).HasColumnName("SPId");
            entity.Property(e => e.Spread).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.TrancheClass).HasMaxLength(5);
            entity.Property(e => e.Value).HasColumnType("decimal(19, 5)");
            entity.Property(e => e.WeightedAverageLife).HasColumnType("decimal(19, 5)");

            entity.HasOne(d => d.RefBenchmark).WithMany(p => p.Tranches)
                .HasForeignKey(d => d.RefBenchmarkId)
                .HasConstraintName("FK_Tranches_LoanReferenceRates");

            entity.HasOne(d => d.Transaction).WithMany(p => p.Tranches)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FKFEBCB7A0AD2A1557");
        });

        modelBuilder.Entity<TrancheCompanyRelationship>(entity =>
        {
            entity.HasKey(e => e.TrancheCompanyRelationshipId).HasName("PK__TrancheC__56EFA3362F2C5BDB");

            entity.HasIndex(e => e.CompanyId, "idx_TrancheCompanyRelationshipCompany").HasFillFactor(80);

            entity.HasIndex(e => e.LeagueTableCreditId, "idx_TrancheCompanyRelationshipLeagueTableCredit").HasFillFactor(80);

            entity.HasIndex(e => e.TranchCompanyRelationshipSubRoleId, "idx_TrancheCompanyRelationshipTranchCompanyRelationshipSubRole").HasFillFactor(80);

            entity.HasIndex(e => e.TrancheId, "idx_TrancheCompanyRelationshipTranche").HasFillFactor(80);

            entity.HasIndex(e => e.TranchCompanyRelationshipRoleId, "idx_TrancheCompanyRelationshipTrancheCompanyRelationshipRole").HasFillFactor(80);

            entity.Property(e => e.EquityValue).HasColumnType("decimal(19, 5)");
            entity.Property(e => e.LeagueTableCreditValue).HasColumnType("decimal(19, 5)");
            entity.Property(e => e.Percentage).HasColumnType("decimal(19, 5)");
            entity.Property(e => e.UnderwrittenValue).HasColumnType("decimal(19, 5)");

            entity.HasOne(d => d.Company).WithMany(p => p.TrancheCompanyRelationshipCompanies)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK2C60EB77961EB50A");

            entity.HasOne(d => d.LeagueTableCredit).WithMany(p => p.TrancheCompanyRelationshipLeagueTableCredits)
                .HasForeignKey(d => d.LeagueTableCreditId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK2C60EB77ABA87A17");

            entity.HasOne(d => d.Tranche).WithMany(p => p.TrancheCompanyRelationships)
                .HasForeignKey(d => d.TrancheId)
                .HasConstraintName("FKCBFF66ACFF030609");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A6B343D6508");

            entity.HasIndex(e => e.Live, "IX_Transactions_Live_INC2");

            entity.HasIndex(e => e.Assignee, "idx_TransactionAssignee");

            entity.HasIndex(e => e.CashflowModelId, "idx_TransactionCashflowModel");

            entity.HasIndex(e => e.CreatedBy, "idx_TransactionCreatedBy");

            entity.HasIndex(e => e.TransactionFinanceTypeId, "idx_TransactionFinanceType");

            entity.HasIndex(e => e.RejectedBy, "idx_TransactionRejectedBy");

            entity.HasIndex(e => e.TransactionConcessionTypeId, "idx_TransactionTransactionConcessionType");

            entity.HasIndex(e => e.TransactionProcurementStageId, "idx_TransactionTransactionProcurementStage");

            entity.HasIndex(e => e.TransactionStageId, "idx_TransactionTransactionStage");

            entity.HasIndex(e => e.TransactionTypeId, "idx_TransactionTransactionType");

            entity.HasIndex(e => e.LastUpdatedByUserId, "idx_TransactionUserId");

            entity.HasIndex(e => e.DraftedBy, "idx_TransactionsDraftedBy");

            entity.HasIndex(e => e.PublishedBy, "idx_TransactionsPublishedBy");

            entity.HasIndex(e => e.SubmittedBy, "idx_TransactionsSubmittedBy");

            entity.HasIndex(e => e.UnpublishedBy, "idx_TransactionsUnpublishedBy");

            entity.Property(e => e.AlternateName).HasMaxLength(200);
            entity.Property(e => e.BankBenchMarkId).HasColumnName("BankBenchMarkID");
            entity.Property(e => e.BankOfferPrice).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.BankPricing).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.CashflowNotes).HasMaxLength(255);
            entity.Property(e => e.ConcessionDuration).HasColumnType("decimal(19, 5)");
            entity.Property(e => e.ConcessionEndDate).HasColumnType("datetime");
            entity.Property(e => e.ConcessionNotes).HasMaxLength(255);
            entity.Property(e => e.ConcessionStartDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasDefaultValue(1);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DateAdded).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.Debt).HasColumnType("decimal(19, 5)");
            entity.Property(e => e.DebtEquityRatio).HasMaxLength(255);
            entity.Property(e => e.DraftedOn).HasColumnType("datetime");
            entity.Property(e => e.Equity).HasColumnType("decimal(19, 5)");
            entity.Property(e => e.LeaseEndDate).HasColumnType("datetime");
            entity.Property(e => e.LeaseStartDate).HasColumnType("datetime");
            entity.Property(e => e.LeaseTerm).HasColumnType("decimal(4, 1)");
            entity.Property(e => e.MarketTypeId).HasColumnName("MarketTypeID");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Ppp).HasColumnName("PPP");
            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");
            entity.Property(e => e.ProjectCreationUserId).HasColumnName("ProjectCreationUserID");
            entity.Property(e => e.PublishedOn).HasColumnType("datetime");
            entity.Property(e => e.Rbl)
                .HasDefaultValue(false)
                .HasColumnName("RBL");
            entity.Property(e => e.RejectedOn).HasColumnType("datetime");
            entity.Property(e => e.RejectionReasons).HasMaxLength(4000);
            entity.Property(e => e.Rfpclosed).HasColumnName("RFPClosed");
            entity.Property(e => e.RfpconsideredStructures)
                .HasMaxLength(30)
                .HasColumnName("RFPConsideredStructures");
            entity.Property(e => e.SelectedCurrency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Spread).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Spv)
                .HasMaxLength(255)
                .HasColumnName("SPV");
            entity.Property(e => e.Status).HasDefaultValue(3);
            entity.Property(e => e.StructureId).HasColumnName("StructureID");
            entity.Property(e => e.SubStructureId).HasColumnName("SubStructureID");
            entity.Property(e => e.SubmittedOn).HasColumnType("datetime");
            entity.Property(e => e.TransactionInstrumentTypeId).HasColumnName("TransactionInstrumentTypeID");
            entity.Property(e => e.UnpublishedOn).HasColumnType("datetime");
            entity.Property(e => e.Value).HasColumnType("decimal(19, 5)");

            entity.HasOne(d => d.BankBenchMark).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.BankBenchMarkId)
                .HasConstraintName("FK_Transactions_LoanReferenceRates");

            entity.HasOne(d => d.MarketType).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.MarketTypeId)
                .HasConstraintName("FK_Transactions_MarketType");

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.ProductCategoryId)
                .HasConstraintName("FK_Transactions_ProductCategory");

            entity.HasOne(d => d.ProductType).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.ProductTypeId)
                .HasConstraintName("FK_Transactions_FinanceTypes");

            entity.HasOne(d => d.Structure).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.StructureId)
                .HasConstraintName("FK_Transactions_Structure");

            entity.HasOne(d => d.SubStructure).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.SubStructureId)
                .HasConstraintName("FK_Transactions_SubStructure");

            entity.HasOne(d => d.TransactionInstrumentType).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.TransactionInstrumentTypeId)
                .HasConstraintName("FK_Transactions_TransactionInstrumentType");

            entity.HasOne(d => d.TransactionProcurementStage).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.TransactionProcurementStageId)
                .HasConstraintName("FK2045D237ADD1496F");

            entity.HasOne(d => d.TransactionType).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.TransactionTypeId)
                .HasConstraintName("FK2045D2379D8970F7");
        });

        modelBuilder.Entity<TransactionInstrumentType>(entity =>
        {
            entity.HasKey(e => e.TransactionInstrumentTypeId).HasName("PK_DealInstrumentType");

            entity.ToTable("TransactionInstrumentType");

            entity.Property(e => e.TransactionInstrumentTypeId).HasColumnName("TransactionInstrumentTypeID");
            entity.Property(e => e.TransactionInstrumentTypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TransactionProcurementStage>(entity =>
        {
            entity.HasKey(e => e.TransactionProcurementStageId).HasName("PK__Transact__9B033E482B947552");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.HasKey(e => e.TransactionTypeId).HasName("PK__Transact__20266D0B3335971A");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
