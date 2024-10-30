using ArticleGeneration.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGeneration.Tests
{
    public class OpenAITest
    {
        private Mock<IOpenAIService>? _service;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IOpenAIService>();
        }

        [Test]
        public async Task GenerateArticleAsyncTest()
        {
            string prompt = "Write an article about unit testing in C#.";
            string expectedArticle = "This is a generated article about unit testing in C#.";

            _service!.Setup(service => service.GenerateArticleAsync(prompt)).ReturnsAsync(expectedArticle);

            var result = await _service.Object.GenerateArticleAsync(prompt);

            Assert.That(result, Is.EqualTo(expectedArticle));
        }
    }
}
