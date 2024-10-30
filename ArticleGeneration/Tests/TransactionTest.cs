using ArticleGeneration.Models;
using ArticleGeneration.Repositories;
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

        [SetUp]
        public void SetUp()
        {
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
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
    }
}
