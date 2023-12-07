using CloudStorage.Core;
using CloudStorage.Core.Model.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudStorage.Test.CloudStorageAPI.Services
{
    [TestFixture]
    public class PokemonServiceTest
    {
        [Test]
        [TestCase(1)]
        public async Task GetAsync_ValidObject_ReturnsPokemon(int pokemonId)
        {
            //Arrange
            SetUpPokemonService();
            var resultRepo = GetPokemonFakeObject();
            SetUpStorageDownloadAsync(resultRepo.Photo);
            SetUpRepositoryGetAsync(pokemonId);

            // Act
            var response = await _pokemonService.GetAsync(pokemonId);

            // Assert
            Assert.IsInstanceOf<PokemonQueryDAL>(response);
            Assert.NotNull(response.Id);
            Assert.NotZero(response.Id);
        }

        [Test]
        public async Task GetAllAsync_ValidObject_ReturnCreatedPokemon()
        {
            // Arrange
            SetUpPokemonService();
            SetUpRepositoryGetAllAsync();

            // Act
            var response = await _pokemonService.GetAllAsync();

            // Assert
            Assert.IsInstanceOf<List<PokemonQueryDAL>>(response);
            Assert.NotNull(response);
        }

        [Test]
        [TestCase(1, "pokemonTest", "testPhoto")]
        public async Task AddAsync_ValidObject_ReturnCreatedPokemon(int pokemonId, string caseName, string casePhoto)
        {
            // Arrange
            SetUpPokemonService();          

            var requestObject = GetPokemonUpsertDALMockObject(caseName, casePhoto);

            SetUpStorageUploadAsync(requestObject.Photo);
            SetUpRepositoryAddAsync();            

            // Act
            var response = await _pokemonService.AddAsync(requestObject);

            // Assert
            Assert.IsInstanceOf<PokemonQueryDAL>(response);
            Assert.NotNull(response.Id);
            Assert.NotNull(response.Name);
        }

        private void SetUpRepositoryAddAsync()
        {
            var requestRepository = new Pokemon
            {
                Name = "pokemonTest",
                Photo = "test Pohot"
            };

            var responseRepository = new Pokemon
            {
                Id = 1,
                Name = "pokemonTest",
                Photo = "test Pohot"
            };

            _mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Pokemon>())).ReturnsAsync(responseRepository);
        }

        private void SetUpStorageUploadAsync(IFormFile photo)
        {
            _mockStorage.Setup(stor => stor.UploadAsync(photo)).ReturnsAsync(GetPokemonQueryDAL().Photo);
        }        

        private void SetUpRepositoryGetAsync(int pokemonId)
        {
            _mockRepo.Setup(repo => repo.GetAsync(pokemonId)).ReturnsAsync(GetPokemonFakeObject());
        }

        private void SetUpStorageDownloadAsync(string photo)
        {
            _mockStorage.Setup(stor => stor.DownloadAsync(photo!)).ReturnsAsync("testPhoto.png");
        }

        private void SetUpPokemonService()
        {
            _mockStorage = new Mock<IStorageManager>();
            _mockRepo = new Mock<IPokemonRepository>();
            _pokemonService = new PokemonService(_mockRepo.Object, _mockStorage.Object);
        }      

        private void SetUpRepositoryGetAllAsync()
        {
            var resultRepo = GetListPokemonFakeObject();

            _mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(resultRepo);
        }

        private IEnumerable<Pokemon> GetListPokemonFakeObject()
        {
            return new List<Pokemon> { 
                new Pokemon {
                    Id = 1,
                    Name = "pokemonName",
                    Photo = "testPhoto.png" },
                new Pokemon {
                    Id = 2,
                    Name = "pokemonNewName",
                    Photo = "testPhoto2.png"
                } };
        }

        private PokemonUpsertDAL GetPokemonUpsertDALMockObject(string caseName, string casePhoto)
        {
            //Setup mock file using a memory stream
            var content = "Returning pokemon data";
            var fileName = casePhoto + ".png";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            IFormFile formFile = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            return new PokemonUpsertDAL()
            {
                Name = caseName,
                Photo = formFile
            };
        }
        private PokemonQueryDAL GetPokemonQueryDAL()
        {
            return new PokemonQueryDAL()
                {
                    Id = 1,
                    Name = "pokemonName",
                    Photo = "testPhoto.png"
                };
        }

        private Pokemon GetPokemonFakeObject()
        {
            return new Pokemon
            {
                Id = 1,
                Name = "testPokemon",
                Photo = "TestPhoto.png"
            };
        }

        // Arrange
        Mock<IStorageManager> _mockStorage;
        Mock<IPokemonRepository> _mockRepo;
        PokemonService _pokemonService;
    }
}
