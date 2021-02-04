using CoursesTests.ExternalAdapters.Http;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoursesTests.ExternalAdapters.Tests.Http
{
    [TestClass]
    public class HttpClientAdapterTests
    {
        private Mock<HttpMessageHandler> _httpMessageHandler;

        private Mock<IHttpClientFactory> _httpClientFactory;

        private HttpClientAdapter _httpClientAdapter;

        [TestInitialize]
        public void Initialize()
        {
            _httpMessageHandler = new Mock<HttpMessageHandler>();

            _httpClientFactory = new Mock<IHttpClientFactory>();

            _httpClientAdapter = new HttpClientAdapter(_httpClientFactory.Object);
        }

        [TestMethod]
        public async Task PostAsync_ShouldPostSuccessfully()
        {
            //Arrange
            _httpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK
                });

            var httpClient = new HttpClient(_httpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            _httpClientFactory.Setup(x => x.CreateClient("Test")).Returns(httpClient);

            //Act
            await _httpClientAdapter.PostAsync("Test", "/tests", new { Id = 1, Name = "Name" });

            //Assert
            _httpMessageHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(request => 
                request.Method == HttpMethod.Post
                //&& request.Content == new StringContent("{\"Id\":1,\"Name\":\"Name\"}", Encoding.UTF8, "application/json")
                && request.RequestUri == new Uri("http://test.com/tests")
                ), ItExpr.IsAny<CancellationToken>());
        }

        [TestMethod]
        public async Task PostAsync_ShouldThrowExceptionIfResponseIsNotSuccessful()
        {
            //Arrange
            _httpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError
                });

            var httpClient = new HttpClient(_httpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            _httpClientFactory.Setup(x => x.CreateClient("Test")).Returns(httpClient);

            var expectedResult = new HttpRequestException("Response status code does not indicate success: 500 (Internal Server Error).");

            //Act
            var result = await Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await _httpClientAdapter.PostAsync("Test", "/tests", new { Id = 1, Name = "Name" }));

            //Assert
            _httpMessageHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(request =>
                request.Method == HttpMethod.Post
                //&& request.Content == new StringContent("{\"Id\":1,\"Name\":\"Name\"}", Encoding.UTF8, "application/json")
                && request.RequestUri == new Uri("http://test.com/tests")
                ), ItExpr.IsAny<CancellationToken>());

            result.Should().BeOfType(expectedResult.GetType());
            result.Message.Should().Be(expectedResult.Message);
        }
    }
}
