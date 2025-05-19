using BookingSystem.BAL.Interfaces;
using BookingSystem.DAL.Interfaces;
using BookingSystemModel.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.BAL.Services
{
    public class BookingBAL : IBookingBAL
    {
        private readonly IInventoryClient _inventoryClient;
        private readonly IMemberDAL _memberDAL;
        private const int MAX_BOOKINGS = 2;

        public BookingBAL(IInventoryClient inventoryClient, IMemberDAL memberDAL)
        {
            _inventoryClient = inventoryClient;
            _memberDAL = memberDAL;
        }

        public async Task<BookingResponse> CreateBooking(CreateBookingRequest request)
        {
            BookingResponse bookingResponse = new BookingResponse();
            Booking bookingInfo;
            try
            {
                var inventoryDetails = _inventoryClient.GetInventoryItem_ByIdAsync(request.InventoryItemId).Result;
                var memberDetails = _memberDAL.GetMember_ByIdAsync(request.MemberId).Result;
                if (inventoryDetails.RemainingCount == 0)
                {
                    bookingResponse.IsCancelled = true;
                    bookingResponse.BookingReference = "Inventory item is not available.";
                }
                if (memberDetails.BookingCount >= MAX_BOOKINGS)
                {
                    bookingResponse.IsCancelled = true;
                    bookingResponse.BookingReference = $"Member has reached the maximum of {MAX_BOOKINGS} active bookings.";
                }
                else
                {
                    inventoryDetails.RemainingCount--;
                    var inventoryInfo = await _inventoryClient.UpdateInventoryInfoAsync(inventoryDetails);

                    memberDetails.BookingCount++;
                    memberDetails.DateJoined = DateTime.UtcNow;
                    var memberInfo = await _memberDAL.UpdateMemberInfoAsync(memberDetails);

                    if (inventoryInfo == true && memberInfo == true)
                    {
                        bookingResponse.BookingReference = Guid.NewGuid().ToString();
                        bookingResponse.IsCancelled = false;
                        bookingResponse.BookingDateTime = memberDetails.DateJoined;
                        bookingResponse.MemberId = memberDetails.Id;
                        bookingResponse.InventoryItemId = inventoryDetails.Id;
                    }

                }
                return bookingResponse;
            }
            catch (Exception ex)
            {
                return new BookingResponse();
            }
        }

        public async Task<BookingResponse> CancelBooking(CreateBookingRequest request)
        {
            BookingResponse bookingResponse = new BookingResponse();
            Booking bookingInfo;
            try
            {
                var inventoryDetails = _inventoryClient.GetInventoryItem_ByIdAsync(request.InventoryItemId).Result;
                var memberDetails = _memberDAL.GetMember_ByIdAsync(request.MemberId).Result;

                inventoryDetails.RemainingCount++;
                var inventoryInfo = await _inventoryClient.UpdateInventoryInfoAsync(inventoryDetails);

                memberDetails.BookingCount--;
                memberDetails.DateJoined = DateTime.UtcNow;
                var memberInfo = await _memberDAL.UpdateMemberInfoAsync(memberDetails);

                if (inventoryInfo == true && memberInfo == true)
                {
                    bookingResponse.BookingReference = Guid.NewGuid().ToString();
                    bookingResponse.IsCancelled = true;
                    bookingResponse.BookingCancelDateTime = memberDetails.DateJoined;
                    bookingResponse.MemberId = memberDetails.Id;
                    bookingResponse.InventoryItemId = inventoryDetails.Id;
                }

                return bookingResponse;
            }
            catch (Exception ex)
            {
                return new BookingResponse();
            }
        }
    }
}
