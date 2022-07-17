using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Swapfiets.Theft.Api;
using Swapfiets.Theft.Service.Models;
using Swapfiets.Theft.Service.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Swapfiets.Theft.Tests.Controllers
{
    public class BikeTheftControllerTests
    {
        private readonly BikeTheftResponse bikeTheftFakes = FakeData.BikeTheftFakes;
        private readonly Mock<IBikeTheftService> mockBikeTheftService;
        private readonly BikeTheftController bikeTheftController;
        public BikeTheftControllerTests()
        {
            mockBikeTheftService = new Mock<IBikeTheftService>();
            bikeTheftController = new BikeTheftController(mockBikeTheftService.Object);
            bikeTheftController.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        [Theory]
        [InlineData("Amsterdam", 20)]
        [InlineData("Amsterdam", 0)]
        public async Task Should_Return_ListOf_TheftBikes_When_SearchByCity(string city, int distance)
        {
            // Arrange
            mockBikeTheftService
                .Setup(s => s.SearchAsync(It.IsAny<BikeTheftQueryParams>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(bikeTheftFakes);

            // Act
            var result = await bikeTheftController.GetBikeThefts(city, null, null, distance, 20, 1);
            var okResult = result as ObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult?.StatusCode);
            Assert.NotNull(bikeTheftFakes);
            Assert.Equal(bikeTheftFakes!.Bikes!.Count(), ((BikeTheftResponse)okResult!.Value!).Bikes!.Count());
        }

        [Theory]
        [InlineData(50.230, 13.4050, 20)]
        [InlineData(23.430, 55.4050, 0)]
        public async Task Should_Return_ListOf_TheftBikes_When_SearchByGeoCoordinate(double latitude, double longitude, int distance)
        {
            // Arrange
            mockBikeTheftService
                .Setup(s => s.SearchAsync(It.IsAny<BikeTheftQueryParams>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(bikeTheftFakes);

            // Act
            var result = await bikeTheftController.GetBikeThefts(null, latitude, longitude, distance,20,1);
            var okResult = result as ObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult?.StatusCode);
            Assert.NotNull(bikeTheftFakes);
            Assert.Equal(bikeTheftFakes!.Bikes!.Count(), ((BikeTheftResponse)okResult!.Value!).Bikes!.Count());
        }

        [Fact]
        public async Task Should_Return_BadRequest_When_BikeTheft_ResponseIsEmpty()
        {
            // Arrange
            mockBikeTheftService
                .Setup(s => s.SearchAsync(It.IsAny<BikeTheftQueryParams>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<BikeTheftResponse>(null));

            // Act
            var result = await bikeTheftController.GetBikeThefts("Amsterdam", null, null, distance: 20, 20, 1);
            var badRequestResult = result as BadRequestResult;

            // Assert
            Assert.IsType<BadRequestResult>(badRequestResult);
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult!.StatusCode);
        }

        [Theory]
        [InlineData("Amsterdam", 20)]
        [InlineData("Amsterdam", 0)]
        public async Task Should_Return_BikeTheftCount_When_SearchByCity(string city, int distance)
        {
            // Arrange
            mockBikeTheftService
                .Setup(s => s.SearchCountAsync(It.IsAny<BikeTheftQueryParams>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BikeTheftCountResponse(70, "", 200));

            // Act
            var result = await bikeTheftController.GetBikeTheftCount(city, null, null, distance);
            var okResult = result as ObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult?.StatusCode);
            Assert.Equal(new BikeTheftCountResponse(70, "", 200), okResult?.Value);
        }

        [Theory]
        [InlineData(50.230, 13.4050, 20)]
        [InlineData(23.430, 55.4050, 0)]
        public async Task Should_Return_BikeTheftCount_When_SearchByGeoCoordinate(double latitude, double longitude, int distance)
        {
            // Arrange
            mockBikeTheftService
                .Setup(s => s.SearchCountAsync(It.IsAny<BikeTheftQueryParams>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BikeTheftCountResponse(70, "", 200));

            // Act
            var result = await bikeTheftController.GetBikeTheftCount(null, latitude, longitude, distance);
            var okResult = result as ObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult?.StatusCode);
            Assert.Equal(new BikeTheftCountResponse(70, "", 200), okResult?.Value);
        }

        [Fact]
        public async Task Should_Return_BadRequest_When_BikeTheftCount_ResponseIsEmpty()
        {
            // Arrange
            mockBikeTheftService
                .Setup(s => s.SearchCountAsync(It.IsAny<BikeTheftQueryParams>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<BikeTheftCountResponse>(null));

            // Act
            var result = await bikeTheftController.GetBikeTheftCount("Amsterdam", null, null, 0);
            var badRequestResult = result as BadRequestResult;

            // Assert
            Assert.IsType<BadRequestResult>(badRequestResult);
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult!.StatusCode);
        }
    }
}
