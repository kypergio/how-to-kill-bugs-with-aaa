using CloudStorage.Core;
using CloudStorage.Core.StorageManagers;
using CloudStorage.Core.Utils;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudStorage.Test.CloudStorageAPI.StorageManagers
{
    public class AzureStorageManagerTest
    {
        
        private Mock<IEnvironmentManager>? _stubEnvironmentManager;
        private Mock<IBlobClient>? _stubBlobClient;
        private Mock<IFileWrapper>? _stubFileWrapper;

        const string WEB_ROOT_PATH = "C:/my-ruta-fake";
        const string APPLICATION_HOST = "http://localhost/";

        #region private_helpers
        private void BuildStubEnvironmentManager()
        {
            _stubEnvironmentManager = new Mock<IEnvironmentManager>();
        }

        private void BuildStubBlobClient()
        {
            _stubBlobClient = new Mock<IBlobClient>();
        }

        private void BuildStubFileWrapper()
        {
            _stubFileWrapper = new Mock<IFileWrapper>();
        }

        private void SetupStubEnvironmentManagerWebRootPath(string returnValue)
        {
            _stubEnvironmentManager?
                .Setup(c => c.WebRootPath)
                .Returns(returnValue);
        }

        private void SetupStubEnvironmentManagerApplicationHost(string returnValue)
        {
            _stubEnvironmentManager?
                .Setup(c => c.ApplicationHost)
                .Returns(returnValue);
        }

        private void SetupStubFileWrapperExists(bool returnValue)
        {
            _stubFileWrapper?
                .Setup(c => c.Exists(It.IsAny<string>()))
                .Returns(returnValue);
        }
        #endregion

        [Test]
        [TestCase("/one-segment/", "68f397f7-d58d-41f4-9d7e-677d2982ee9a")]
        [TestCase("/one-segment/two-segments/", "ad8a717b-da22-497e-9a95-b75e68a51457")]
        [TestCase("/", "ad8a717b-da22-497e-9a95-b75e68a51457")]
        public async Task DownloadAsync_VerifyBlobClientExecution(string path, string fileName)
        {
            #region Arrange
            BuildStubEnvironmentManager();
            BuildStubBlobClient();
            BuildStubFileWrapper();

            SetupStubEnvironmentManagerWebRootPath(returnValue: WEB_ROOT_PATH);
            SetupStubEnvironmentManagerApplicationHost(returnValue: APPLICATION_HOST);
            SetupStubFileWrapperExists(returnValue: false);
            #endregion

            #region Act
            var actor = new AzureStorageManager(_stubEnvironmentManager!.Object, _stubBlobClient!.Object, _stubFileWrapper!.Object);
            var actual = await actor.DownloadAsync($"{path}{fileName}");
            #endregion

            #region Assert
            _stubBlobClient.Verify(c => c.DownloadAsync(fileName), Times.Once);
            #endregion
        }
    }
}
