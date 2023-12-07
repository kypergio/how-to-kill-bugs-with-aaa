using CloudStorage.Core;
using CloudStorage.Core.Exceptions;
using CloudStorage.Core.Model.DAL;
using CloudStorageAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudStorage.Test.CloudStorageAPI.Controllers
{
    [TestFixture]
    public class PokemonControllerTests
    {
        private Mock<IPokemonService> stubService;
        private PokemonController stubController;

        private void CreateDeafultFakes()
        {
            stubService = new Mock<IPokemonService>();
            stubController = new PokemonController(stubService.Object);
        }

        private PokemonUpsertDAL SetupServiceWithResponse(
            int caseId,
            string casePhoto,
            string caseName
        )
        {
            CreateDeafultFakes();
            var requestObject = new PokemonUpsertDAL() { Name = caseName, Photo = null };
            PokemonQueryDAL fakeData =
                new()
                {
                    Id = caseId,
                    Name = caseName,
                    Photo = casePhoto
                };
            stubService.Setup(service => service.AddAsync(requestObject)).ReturnsAsync(fakeData);
            return requestObject;
        }

        private PokemonUpsertDAL SetufFailingService()
        {
            CreateDeafultFakes();
            var requestObject = new PokemonUpsertDAL() { Name = null, Photo = null };

            stubService.Setup(service => service.AddAsync(requestObject)).Throws(new Exception());
            return requestObject;
        }

        private void SetupServiceWithData(
            int requestPokemonId,
            string requestPokemonPhoto,
            string requestPokemonName
        )
        {
            CreateDeafultFakes();
            stubService
                .Setup(service => service.GetAsync(requestPokemonId))
                .ReturnsAsync(
                    new PokemonQueryDAL()
                    {
                        Id = requestPokemonId,
                        Photo = requestPokemonPhoto,
                        Name = requestPokemonName
                    }
                );
        }

        private void SetupEmptyService(int requestPokemonId)
        {
            CreateDeafultFakes();

            stubService
                .Setup(service => service.GetAsync(requestPokemonId))
                .Throws(new EntityNotFoundException());
        }

        [Test]
        [TestCase(1, null, "pokemonTest")]
        public async Task Post_When_GivenAValidObject_Then_ReturnsOk(
            int caseId,
            string casePhoto,
            string caseName
        )
        {
            // Arrange
            PokemonUpsertDAL requestObject = SetupServiceWithResponse(caseId, casePhoto, caseName);

            // Act
            var response = await stubController.Post(requestObject);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        public async Task Post_When_GivenAnInvalidObject_Then_ReturnsStatusCode500()
        {
            PokemonUpsertDAL requestObject = SetufFailingService();

            var response = await stubController.Post(requestObject);
            StatusCodeResult statusResult = (StatusCodeResult)response;

            Assert.AreEqual(500, statusResult.StatusCode);
        }

        [Test]
        [TestCase(1, null, "pokemonTest")]
        public async Task Get_When_GivenAnExistingId_Then_ReturnsOk(
            int requestPokemonId,
            string requestPokemonPhoto,
            string requestPokemonName
        )
        {
            SetupServiceWithData(requestPokemonId, requestPokemonPhoto, requestPokemonName);

            var response = await stubController.Get(requestPokemonId);

            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public async Task Get_When_GivenANonExistantId_Then_ReturnsNotFound()
        {
            int requestPokemonId = -1;
            SetupEmptyService(requestPokemonId);

            var response = await stubController.Get(requestPokemonId);

            Assert.IsInstanceOf<NotFoundResult>(response);
        }

        [Test]
        [TestCase(1, null, "pokemonTest")]
        public async Task Get_When_SentADefaultRequest_ReturnsOk(
            int requestPokemonId,
            string requestPokemonPhoto,
            string requestPokemonName
        )
        {
            SetupServiceWithData(requestPokemonId, requestPokemonPhoto, requestPokemonName);

            var response = await stubController.Get();

            Assert.IsInstanceOf<OkObjectResult>(response);
        }
    }
}
