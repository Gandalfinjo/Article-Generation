using ArticleGeneration.Data;
using ArticleGeneration.Repositories;
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
        private const string TestGeneratedText = "This is a test article";
        private const int TestTransactionId = 123;

        [SetUp]
        public void SetUp()
        {
            _articleRepositoryMock = new Mock<IArticleRepository>();
        }

        [Test]
        public async Task AddArticleAsyncTest()
        {
            _articleRepositoryMock!.Setup(repo => repo.AddArticleAsync(TestGeneratedText, TestTransactionId)).Returns(Task.CompletedTask);

            await _articleRepositoryMock!.Object.AddArticleAsync(TestGeneratedText, TestTransactionId);

            _articleRepositoryMock.Verify(repo => repo.AddArticleAsync(TestGeneratedText, TestTransactionId), Times.Once());
        }
    }
}
