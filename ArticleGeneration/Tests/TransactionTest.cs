using ArticleGeneration.Models;
using ArticleGeneration.Repositories;
using ArticleGeneration.Services;
using Castle.Components.DictionaryAdapter.Xml;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGeneration.Tests
{
    public class TransactionTest
    {
        private Mock<ITransactionRepository>? _transactionRepositoryMock;
        private ITransactionService? _transactionServiceMock;

        [SetUp]
        public void SetUp()
        {
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
            _transactionServiceMock = new TransactionService(_transactionRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllTransactionsAsyncTest()
        {
            var transactions = new List<Transaction>
            {
                new Transaction { TransactionId = 1, Name = "Test Transaction 1", Live = true },
                new Transaction { TransactionId = 2, Name = "Test Transaction 2", Live = true}
            };

            _transactionRepositoryMock?.Setup(repo => repo.GetAllTransactionsAsync()).ReturnsAsync(transactions);

            var result = await _transactionRepositoryMock!.Object.GetAllTransactionsAsync();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].TransactionId, Is.EqualTo(1));
            Assert.That(result[1].TransactionId, Is.EqualTo(2));
        }

        [Test]
        public async Task GetNewOrUpdatedTransactionsAsyncTest()
        {
            var lastChecked = DateTime.UtcNow.AddDays(-1);
            var transactions = new List<Transaction>
            {
                new Transaction { TransactionId = 1, DateUpdated = DateTime.UtcNow },
                new Transaction { TransactionId = 2, DateUpdated = DateTime.UtcNow.AddDays(-2) }
            };

            _transactionRepositoryMock!.Setup(repo => repo.GetNewOrUpdatedTransactionsAsync(lastChecked))
                .ReturnsAsync(transactions.FindAll(t => t.DateUpdated >= lastChecked));

            var result = await _transactionRepositoryMock.Object.GetNewOrUpdatedTransactionsAsync(lastChecked);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].TransactionId, Is.EqualTo(1));
        }

        [Test]
        public void GeneratePromptTest()
        {
            var transaction = new Transaction
            {
                TransactionId = 1,
                Name = "Test Transaction 1",
                Live = true,
                Value = 10000,
                Description = "A small transaction",
                TransactionTypeId = 1,
                TransactionProcurementStageId = 1,
                PublishedOn = DateTime.UtcNow,
                SelectedCurrency = "RSD",
                TransactionInstrumentTypeId = 1,
                ProductCategoryId = 1,
                MarketTypeId = 1,
                StructureId = 1,
                BankPricing = 1000,
                BankOfferPrice = 20000,
                BankBenchMarkId = 1,
                ProductTypeId = 1
            };

            var result = _transactionServiceMock!.GeneratePrompt(transaction);

            var expectedPrompt = "Based on the following data about a transaction, write an informative article:\n" +
                                        $"- Transaction Name: {transaction.Name}" +
                                        $"- Transaction Description: {transaction.Description ?? ""}" +
                                        $"- Live: {transaction.Live}" +
                                        $"- Transaction Value: {transaction.Value ?? 0}" +
                                        $"- Transaction Type: {transaction.TransactionType?.Name}" +
                                        $"- Transaction Stage: {transaction.TransactionProcurementStage?.Name}" +
                                        $"- Published On: {transaction.PublishedOn ?? new DateTime()}" +
                                        $"- Selected Currency: {transaction.SelectedCurrency ?? ""}" +
                                        $"- Transaction Instrument Type: {transaction.TransactionInstrumentType?.TransactionInstrumentTypeName}" +
                                        $"- Product Category: {transaction.ProductCategory?.Name}" +
                                        $"- Market Type: {transaction.MarketType?.MarketTypeName}" +
                                        $"- Structure: {transaction.Structure?.StructureName}" +
                                        $"- BankPricing: {transaction.BankPricing ?? 0}" +
                                        $"- BankOfferPrice: {transaction.BankOfferPrice ?? 0}" +
                                        $"- LoanReference Name: {transaction.BankBenchMark?.Name}" +
                                        $"- LoanReference City: {transaction.BankBenchMark?.City}" +
                                        $"- Product Type: {transaction.ProductType?.Name}";

            Assert.That(result, Is.EqualTo(expectedPrompt));
        }
    }
}
