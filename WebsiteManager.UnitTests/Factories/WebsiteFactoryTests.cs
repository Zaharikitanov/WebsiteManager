using Moq;
using NUnit.Framework;
using WebsiteManager.Factories;
using WebsiteManager.Helpers.Interfaces;
using WebsiteManager.Models;
using WebsiteManager.Models.Database;
using WebsiteManager.Models.View;

namespace WebsiteManager.UnitTests.Factories
{
    [TestFixture]
    public class WebsiteFactoryTests
    {
        private const string name = "Cool website name";
        private const string url = "www.coolwebsite.com";
        private const WebsiteCategories category = WebsiteCategories.Commerce;
        private const string homepageSnapshot = "image_here";
        private const string email = "mail@mail.com";
        private const string password = "pass";
        private const string hashedPassword = "hashedPassword";

        private Mock<IStringHash> _stringHash;
        private WebsiteFactory _factory;

        [SetUp]
        public void SetUp()
        {
            _stringHash = new Mock<IStringHash>();

            _stringHash.Setup(s => s.ComputeSha256Hash(password)).Returns(hashedPassword);

            _factory = new WebsiteFactory(
                _stringHash.Object);
        }

        [Test]
        public void ReturnCreatedWebsite()
        {               
            Assert.NotNull(RunFactoryCreate());
        }

        [Test]
        public void CheckIfCreatedWebsiteIsOfExpectedType()
        {
            Assert.IsInstanceOf(typeof(Website), RunFactoryCreate());
        }

        [Test]
        public void CheckForCorrectlyBuiltValues()
        {
            var result = RunFactoryCreate();

            Assert.AreEqual(name, result.Name);
            Assert.AreEqual(url, result.URL);
            Assert.AreEqual(category, result.Category);
            Assert.AreEqual(homepageSnapshot, result.HomepageSnapshot);
            Assert.AreEqual(email, result.Email);
            Assert.AreEqual(hashedPassword, result.Password);
            Assert.AreEqual(false, result.IsDeleted);
        }

        private Website RunFactoryCreate() {

           return _factory.Create(
             new WebsiteInputData()
             {
                 Name = name,
                 URL = url,
                 Category = category,
                 HomepageSnapshot = homepageSnapshot,
                 LoginDetails = new LoginDetails()
                 {
                     Email = email,
                     Password = password
                 },
             });
        } 
    }
}
