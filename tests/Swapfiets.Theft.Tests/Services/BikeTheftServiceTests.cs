using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Swapfiets.Theft.Service.Models;
using Swapfiets.Theft.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Swapfiets.Theft.Tests.Services
{
    public class BikeTheftServiceTests
    {
        private readonly string bikeTheftJson = FakeData.BikeTheftJson;
        private readonly IBikeTheftService bikeTheftService;
        private readonly Mock<IHttpClientFactory> mockHttpClientFactory;
        private readonly Mock<IConfiguration> mockConfiguration;

        public BikeTheftServiceTests()
        {
            Mock<IConfigurationSection> mockMaxRetriesSection = GetMaxRetriesSection();

            mockConfiguration = new Mock<IConfiguration>();
            mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockConfiguration.Setup(x => x.GetSection("MaxRetries")).Returns(mockMaxRetriesSection.Object);
            bikeTheftService = new BikeTheftService(mockConfiguration.Object, mockHttpClientFactory.Object);
        }

        private static Mock<IConfigurationSection> GetMaxRetriesSection()
        {
            var mockIConfigurationSection = new Mock<IConfigurationSection>();
            mockIConfigurationSection.Setup(x => x.Key).Returns("MaxRetries");
            mockIConfigurationSection.Setup(x => x.Value).Returns("3");
            return mockIConfigurationSection;
        }

        [Fact]
        public async Task Search_Should_Throws_ArgumentExceptions_With_Null_And_Invalid_Parameters()
        {
            // Act + Assert

            // Throws exception when filtered parameters are null
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await bikeTheftService.SearchAsync(null, CancellationToken.None));

            // Throws exception when no city or GeoCoordinate are provided
            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await bikeTheftService.SearchAsync(new BikeTheftQueryParams(null, null, 0), CancellationToken.None));

            // Throws exception when invalid GeoCoordinate is provided
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                await bikeTheftService.SearchAsync(new BikeTheftQueryParams("", new GeoCoordinate(-150.230, 14.234), 0), CancellationToken.None));
        }

        [Fact]
        public async Task Search_Should_Return_BikeTheftResponse_With_City_Parameter()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(bikeTheftJson),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            // Act
            var result = await bikeTheftService.SearchAsync(new BikeTheftQueryParams("Amsterdam", null, 0), CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BikeTheftResponse>(result);
            Assert.Single(result!.Bikes);
        }

        [Fact]
        public async Task SearchCount_Should_Throws_ArgumentExceptions_With_Null_And_Invalid_Parameters()
        {
            // Act + Assert

            // Throws exception when filtered parameters are null
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await bikeTheftService.SearchCountAsync(null, CancellationToken.None));

            // Throws exception when no city or GeoCoordinate are provided
            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await bikeTheftService.SearchCountAsync(new BikeTheftQueryParams(null, null, 0), CancellationToken.None));

            // Throws exception when invalid GeoCoordinate is provided
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                await bikeTheftService.SearchCountAsync(new BikeTheftQueryParams("", new GeoCoordinate(-150.230, 14.234), 0), CancellationToken.None));
        }

        [Fact]
        public async Task SearchCount_Should_Return_BikeTheftResponse_With_City_Parameter()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{ 'count': 10 }"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            // Act
            var result = await bikeTheftService.SearchCountAsync(new BikeTheftQueryParams("Amsterdam", null, 0), CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BikeTheftCountResponse>(result);
            Assert.Equal(200, result!.StatusCode);
        }
    }
}
