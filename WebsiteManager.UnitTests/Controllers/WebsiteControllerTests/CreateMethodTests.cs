using Moq;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;
using WebsiteManager.Controllers;
using WebsiteManager.Factories.Interfaces;
using WebsiteManager.Models;
using WebsiteManager.Models.View;
using WebsiteManager.Services.Interfaces;

namespace WebsiteManager.UnitTests.Controllers.WebsiteControllerTests
{
    [TestFixture]
    public class CreateMethodTests
    {
        private WebsiteInputData inputData = new WebsiteInputData();
        private Mock<IWebsiteService> _service;
        private Mock<IStatusCodeResultFactory> _resultFactory;
        private WebsiteController _controller;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IWebsiteService>();
            _service.SetReturnsDefault(Task.FromResult(EntityActionOutcome.Success));
            _resultFactory = new Mock<IStatusCodeResultFactory>();

            _controller = new WebsiteController(_service.Object, _resultFactory.Object);
        }


        [Test]
        public async Task UseTheCreateEntityAsyncToCreateNewEntity()
        {
            await _controller.Create(inputData);

            _service.Verify(s => s.CreateEntityAsync(inputData), Times.Once);
            _service.VerifyNoOtherCalls();
        }

        [Test]
        public async Task ReturnAnOkStatusCodeResultWhenCreateEntityOutcomeIsSuccess()
            => await TestReturnsCorrectStatusCode(EntityActionOutcome.Success, HttpStatusCode.OK);

        [Test]
        public async Task ReturnAConflictStatusCodeResultWhenCreateEntityOutcomeIsCreateFailed()
            => await TestReturnsCorrectStatusCode(EntityActionOutcome.CreateFailed, HttpStatusCode.Conflict);

        [Test]
        public async Task ReturnUnprocessableEntityStatusCodeWhenEntityActionOutcomeIsMissingFullEntityData()
            => await TestReturnsCorrectStatusCode(EntityActionOutcome.MissingFullEntityData, HttpStatusCode.UnprocessableEntity);

        [TestCase((EntityActionOutcome)19)]
        [TestCase((EntityActionOutcome)17)]
        [TestCase((EntityActionOutcome)18)]
        public async Task ReturnAnInternalServerErrorStatusCodeResultWhenOutcomeIsNotRecognized(EntityActionOutcome outcome)
            => await TestReturnsCorrectStatusCode(outcome, HttpStatusCode.InternalServerError);

        private async Task TestReturnsCorrectStatusCode(EntityActionOutcome outcome, HttpStatusCode statusCode)
        {
            _service.SetReturnsDefault(Task.FromResult(outcome));
            _resultFactory.Setup(x => x.Create(outcome)).Returns(statusCode);
            var response = await _controller.Create(inputData);

            Assert.AreEqual(statusCode, response);
        }
    }
}
