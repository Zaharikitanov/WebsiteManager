using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using WebsiteManager.Factories.Interfaces;
using WebsiteManager.Models;
using WebsiteManager.Models.Database;
using WebsiteManager.Models.View;
using WebsiteManager.Repository.Interfaces;
using WebsiteManager.Services;

namespace WebsiteManager.UnitTests.Services.WebsiteServiceTests
{
    [TestFixture]
    public class CreateEntityAsyncTests
    {
        private Mock<IWebsiteRepository> _repository;
        private Mock<IWebsiteFactory> _factory;

        private WebsiteService _service;

        [SetUp]
        public void SetUp()
        {
            _factory = new Mock<IWebsiteFactory>();
            _repository = new Mock<IWebsiteRepository>();

            _repository.Setup(r => r.AddAsync(It.IsAny<Website>())).ReturnsAsync(new Website());

            _service = new WebsiteService(
                _repository.Object,
                _factory.Object);
        }

        [Test]
        public async Task ReturnSuccessfulCreateEntityOutcome()
        {
            Assert.AreEqual(EntityActionOutcome.Success, await _service.CreateEntityAsync(ValidNewWebsiteData()));
        }

        [Test]
        public async Task ReturnCreateFailedEntityOutcome()
        {
            _repository.Setup(r => r.AddAsync(It.IsAny<Website>())).ReturnsAsync(It.IsAny<Website>());

            Assert.AreEqual(EntityActionOutcome.CreateFailed, await _service.CreateEntityAsync(ValidNewWebsiteData()));
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("This is very long string which is more than a ninety characters long and that is very long indeed")]
        public async Task ReturnMissingFullEntityDataOutcomeWhenNamePropertyValueIsInvalid(string name)
        {
            var invalidEntityData = ValidNewWebsiteData();
            invalidEntityData.Name = name;
            Assert.AreEqual(EntityActionOutcome.MissingFullEntityData, await _service.CreateEntityAsync(invalidEntityData));
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("ww.test")]
        [TestCase("http://.com")]
        public async Task ReturnMissingFullEntityDataOutcomeWhenURLPropertyValueIsInvalid(string url)
        {
            var invalidEntityData = ValidNewWebsiteData();
            invalidEntityData.URL = url;
            Assert.AreEqual(EntityActionOutcome.MissingFullEntityData, await _service.CreateEntityAsync(invalidEntityData));
        }

        [TestCase((WebsiteCategories)14)]
        [TestCase((WebsiteCategories)16)]
        public async Task ReturnMissingFullEntityDataOutcomeWhenCategoryPropertyValueIsInvalid(WebsiteCategories category)
        {
            var invalidEntityData = ValidNewWebsiteData();
            invalidEntityData.Category = category;
            Assert.AreEqual(EntityActionOutcome.MissingFullEntityData, await _service.CreateEntityAsync(invalidEntityData));
        }
        
        [TestCase("")]
        [TestCase(null)]
        [TestCase("This is very long string which is more than a ninety characters long and that is very long indeed")]
        public async Task ReturnMissingFullEntityDataOutcomeWhenHomepageSnapshotPropertyValueIsInvalid(string homepageSnapshot)
        {
            var invalidEntityData = ValidNewWebsiteData();
            invalidEntityData.HomepageSnapshot = homepageSnapshot;
            Assert.AreEqual(EntityActionOutcome.MissingFullEntityData, await _service.CreateEntityAsync(invalidEntityData));
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("mail.com")]
        [TestCase("mail@com")]
        public async Task ReturnMissingFullEntityDataOutcomeWhenEmailPropertyValueIsInvalid(string email)
        {
            var invalidEntityData = ValidNewWebsiteData();
            invalidEntityData.LoginDetails.Email = email;
            Assert.AreEqual(EntityActionOutcome.MissingFullEntityData, await _service.CreateEntityAsync(invalidEntityData));
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("125")]
        [TestCase("thisisverylongpasswordthatisnotrequired")]
        public async Task ReturnMissingFullEntityDataOutcomeWhenPasswordPropertyValueIsInvalid(string password)
        {
            var invalidEntityData = ValidNewWebsiteData();
            invalidEntityData.LoginDetails.Password = password;
            Assert.AreEqual(EntityActionOutcome.MissingFullEntityData, await _service.CreateEntityAsync(invalidEntityData));
        }

        private WebsiteInputData ValidNewWebsiteData()
        {
            return new WebsiteInputData()
            {
                Name = "Test",
                URL = "http://www.test.com",
                Category = WebsiteCategories.Business,
                HomepageSnapshot = "image.jpeg",
                LoginDetails = new LoginDetails()
                {
                    Email = "mail@mail.com",
                    Password = "pass"
                }
            };
        }
    }
}
