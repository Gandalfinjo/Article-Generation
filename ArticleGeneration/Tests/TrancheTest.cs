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
    public class TrancheTest
    {
        private Mock<ITrancheRepository>? _trancheRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _trancheRepositoryMock = new Mock<ITrancheRepository>();
        }

        [Test]
        public async Task GetAllTranchesAsync_RetrievesAllTranchesCorrectly()
        {
            var tranches = new List<Tranche>
            {
                new Tranche { TrancheId = 1, Name = "Test Tranche 1", Value = 100 },
                new Tranche { TrancheId = 2, Name = "Test Tranche 2", Value = 200 }
            };

            _trancheRepositoryMock!.Setup(repo => repo.GetAllTranchesAsync()).ReturnsAsync(tranches);

            var result = await _trancheRepositoryMock.Object.GetAllTranchesAsync();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].TrancheId, Is.EqualTo(1));
            Assert.That(result[1].TrancheId, Is.EqualTo(2));
        }
    }
}
