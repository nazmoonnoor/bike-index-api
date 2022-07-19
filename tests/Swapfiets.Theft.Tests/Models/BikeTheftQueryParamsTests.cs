using Swapfiets.Theft.Service.Models;
using System;
using Xunit;

namespace Swapfiets.Theft.Tests
{
    public class BikeTheftQueryParamsTests
    {
        [Fact]
        public void FuncIsValid_Should_Return_False_When_City_Is_Empty()
        {
            // Arrange + Act
            var queryParams = new BikeTheftQueryParams("", String.Empty, 0);

            // Assert
            Assert.NotNull(queryParams);
            Assert.False(queryParams.IsValid());
        }

        [Fact]
        public void FuncIsValid_Should_Return_True_When_City_IsNot_Empty()
        {
            // Arrange + Act
            var queryParams = new BikeTheftQueryParams("Amsterdam", String.Empty, 0);

            // Assert
            Assert.NotNull(queryParams);
            Assert.True(queryParams.IsValid());
        }

        [Fact]
        public void FuncIsValid_Should_Return_True_When_LatLng_IsNot_Empty()
        {
            // Arrange + Act
            var queryParams = new BikeTheftQueryParams("", "52.230, 23.258", 0);

            // Assert
            Assert.NotNull(queryParams);
            Assert.True(queryParams.IsValid());
        }

        [Fact]
        public void GetGeoCoordinate_Should_Return_Null_When_LatLng_Is_Empty()
        {
            // Arrange + Act
            var queryParams = new BikeTheftQueryParams("", String.Empty, 0);

            // Assert
            Assert.NotNull(queryParams);
            Assert.Null(queryParams.GetGeoCoordinate(string.Empty));
        }

        [Fact]
        public void GetGeoCoordinate_Should_Return_Null_When_LatLng_Is_Invalid()
        {
            // Arrange + Act
            var queryParams = new BikeTheftQueryParams("", String.Empty, 0);

            // Assert
            Assert.NotNull(queryParams);
            Assert.Null(queryParams.GetGeoCoordinate("something"));
        }

        [Fact]
        public void GetGeoCoordinate_Should_Return_Object_When_LatLng_Is_Valid()
        {
            // Arrange + Act
            var queryParams = new BikeTheftQueryParams("", String.Empty, 0);

            // Assert
            Assert.NotNull(queryParams);
            Assert.NotNull(queryParams.GetGeoCoordinate("52.230, 23.564"));
        }
    }
}
