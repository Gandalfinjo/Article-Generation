using ArticleGeneration.Data;
using ArticleGeneration.Repositories;
using ArticleGeneration.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGeneration.Tests
{
    public class ArticleTest
    {
        private Mock<IArticleRepository>? _articleRepositoryMock;
        private Mock<IArticleService>? _articleServiceMock;

        [SetUp]
        public void SetUp()
        {
            _articleRepositoryMock = new Mock<IArticleRepository>();
            _articleServiceMock = new Mock<IArticleService>();
        }

        [Test]
        public async Task AddArticleAsyncTest()
        {
            const string TestGeneratedText = "This is a test article";
            const int TestTransactionId = 123;

            _articleRepositoryMock!.Setup(repo => repo.AddArticleAsync(TestGeneratedText, TestTransactionId)).Returns(Task.CompletedTask);

            await _articleRepositoryMock!.Object.AddArticleAsync(TestGeneratedText, TestTransactionId);

            _articleRepositoryMock.Verify(repo => repo.AddArticleAsync(TestGeneratedText, TestTransactionId), Times.Once());
        }

        [Test]
        public async Task AddArticleAsync_CallsRepository()
        {
            const string TestGeneratedText = "This is a test article";
            const int TestTransactionId = 123;

            _articleRepositoryMock!.Setup(repo => repo.AddArticleAsync(TestGeneratedText, TestTransactionId)).Returns(Task.CompletedTask);

            _articleServiceMock!.Setup(service => service.AddArticleAsync(TestGeneratedText, TestTransactionId))
                .Callback(async (string text, int transactionId) =>
                {
                    await _articleRepositoryMock.Object.AddArticleAsync(text, transactionId);
                });

            await _articleServiceMock.Object.AddArticleAsync(TestGeneratedText, TestTransactionId);

            _articleRepositoryMock.Verify(repo => repo.AddArticleAsync(TestGeneratedText, TestTransactionId), Times.Once());
        }
    }
}
