using NUnit.Framework;
using System;
using WebsiteManager.Mappers;
using WebsiteManager.Models;
using WebsiteManager.Models.Database;
using WebsiteManager.Models.View;

namespace WebsiteManager.UnitTests.Mappers
{
    [TestFixture]
    public class WebsiteDataMapperTests
    {
        private Guid id = new Guid();
        private const string name = "Cool website name";
        private const string url = "www.coolwebsite.com";
        private const WebsiteCategories category = WebsiteCategories.Commerce;
        private const string homepageSnapshot = "image_here";
        private const string email = "mail@mail.com";
        private const string hashedPassword = "hashedPassword";
        private const string createdAt = "11/05/2020";

        private WebsiteDataMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _mapper = new WebsiteDataMapper();
        }

        [Test]
        public void ReturnMappedObject()
        {
            Assert.NotNull(RunMapperMap());
        }

        [Test]
        public void CheckIfMappedObjectIsOfExpectedType()
        {
            Assert.IsInstanceOf(typeof(WebsiteViewData), RunMapperMap());
        }

        [Test]
        public void CheckForCorrectlyMappedValues()
        {
            var result = RunMapperMap();

            Assert.AreEqual(id, result.Id);
            Assert.AreEqual(name, result.Name);
            Assert.AreEqual(url, result.URL);
            Assert.AreEqual(category, result.Category);
            Assert.AreEqual(createdAt, result.CreatedAt);
            Assert.AreEqual(homepageSnapshot, result.HomepageSnapshot);
            Assert.AreEqual(email, result.LoginDetails.Email);
            Assert.AreEqual(hashedPassword, result.LoginDetails.Password);
        }

        private WebsiteViewData RunMapperMap()
        {
            return _mapper.Map(
              new Website()
              {
                  Id = id,
                  Name = name,
                  URL = url,
                  Category = category,
                  CreatedAt = createdAt,
                  HomepageSnapshot = homepageSnapshot,
                  Email = email,
                  Password = hashedPassword
              });
        }
    }
}
