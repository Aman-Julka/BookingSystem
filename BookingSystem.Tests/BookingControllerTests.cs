using BookingSystem.BAL.Interfaces;
using BookingSystem.Controllers;
using BookingSystemModel.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BookingSystem.Tests
{
    public class BookingControllerTests
    {
        private readonly Mock<IBookingBAL> _mockBookingBAL;
        private readonly BookingController _controller;

        public BookingControllerTests()
        {
            _mockBookingBAL = new Mock<IBookingBAL>();
            _controller = new BookingController(_mockBookingBAL.Object);
        }

        [Fact]
        public async Task CreateBooking_ReturnsOkResult_WithBookingResponse()
        {
            // Arrange
            var request = new CreateBookingRequest
            {
                // populate with test data
                MemberId = 5,
                InventoryItemId = 1
            };

            var expectedResponse = new BookingResponse
            {
                Id = 0,
                MemberId = 5,
                InventoryItemId = 1,
                IsCancelled=false
            };

            _mockBookingBAL.Setup(bal => bal.CreateBooking(It.IsAny<CreateBookingRequest>()))
                           .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.CreateBooking(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResponse = Assert.IsType<BookingResponse>(okResult.Value);
            Assert.Equal(expectedResponse.InventoryItemId, actualResponse.InventoryItemId);
            Assert.Equal(expectedResponse.MemberId, actualResponse.MemberId);
            Assert.Equal(expectedResponse.IsCancelled, actualResponse.IsCancelled);
        }

        [Fact]
        public async Task CreateBooking_ReturnsBadRequest_OnException()
        {
            // Arrange
            var request = new CreateBookingRequest
            {
                MemberId = 5,
                InventoryItemId = 6
            };

            _mockBookingBAL.Setup(bal => bal.CreateBooking(It.IsAny<CreateBookingRequest>()))
                           .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.CreateBooking(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Test exception", badRequestResult.Value);
        }

        [Fact]
        public async Task CancelBooking_ReturnsOkResult_WithBookingResponse()
        {
            // Arrange
            var request = new CreateBookingRequest
            {
                // populate with test data
                MemberId = 5,
                InventoryItemId = 1
            };

            var expectedResponse = new BookingResponse
            {
                Id = 0,
                MemberId = 5,
                InventoryItemId = 1,
                IsCancelled = true
            };

            _mockBookingBAL.Setup(bal => bal.CancelBooking(It.IsAny<CreateBookingRequest>()))
                           .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.CancelBooking(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResponse = Assert.IsType<BookingResponse>(okResult.Value);
            Assert.Equal(expectedResponse.InventoryItemId, actualResponse.InventoryItemId);
            Assert.Equal(expectedResponse.MemberId, actualResponse.MemberId);
            Assert.Equal(expectedResponse.IsCancelled, actualResponse.IsCancelled);
        }

        [Fact]
        public async Task CancelBooking_ReturnsBadRequest_OnException()
        {
            // Arrange
            var request = new CreateBookingRequest
            {
                MemberId = 5,
                InventoryItemId = 6
            };

            _mockBookingBAL.Setup(bal => bal.CancelBooking(It.IsAny<CreateBookingRequest>()))
                           .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.CancelBooking(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Test exception", badRequestResult.Value);
        }
    }
}

