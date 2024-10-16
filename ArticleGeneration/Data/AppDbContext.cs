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

    public virtual DbSet<LoanReferenceRate> LoanReferenceRates { get; set; }

    public virtual DbSet<MarketType> MarketTypes { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<Structure> Structures { get; set; }

    public virtual DbSet<SubStructure> SubStructures { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TransactionProcurementStage> TransactionProcurementStages { get; set; }

    public virtual DbSet<TransactionType> TransactionTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

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

            entity.HasOne(d => d.TransactionProcurementStage).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.TransactionProcurementStageId)
                .HasConstraintName("FK2045D237ADD1496F");

            entity.HasOne(d => d.TransactionType).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.TransactionTypeId)
                .HasConstraintName("FK2045D2379D8970F7");
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
