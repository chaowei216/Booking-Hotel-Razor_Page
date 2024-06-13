using BusinessLogic.Interfaces;
using BusinessObject.Models;
using DataAccess.Repository.Interface;
using System;

namespace BusinessLogic.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingService _bookingService;

        public RoomService(IRoomRepository roomRepository, IBookingService bookingService)
        {
            _roomRepository = roomRepository;
            _bookingService = bookingService;
        }

        public async Task<IEnumerable<RoomInformation>> GetAllRooms()
        {
            return await _roomRepository.GetAllRooms();
        }

        public async Task<IEnumerable<RoomInformation>> GetAvailableRoomsByTime(DateTime startDate, DateTime endDate)
        {
            // convert date time to date only
            DateOnly startDateOnly = new DateOnly(startDate.Year, startDate.Month, startDate.Day);
            DateOnly endDateOnly = new DateOnly(endDate.Year, endDate.Month, endDate.Day);

            var currentRooms = await _roomRepository.GetAllRooms();
            var allBookingDetails = await _bookingService.GetAllBookingDetails();

            var bookingInDate = allBookingDetails.Where(p => (p.StartDate <= startDateOnly && p.EndDate >= startDateOnly) 
                                                        || (p.StartDate <= endDateOnly && p.EndDate >= endDateOnly)).Select(p => p.RoomId).Distinct();

            var availableRooms = currentRooms.Where(p => !bookingInDate.Contains(p.RoomId)).ToList();

            return availableRooms;
        }

        public async Task<RoomInformation?> GetRoom(int roomId)
        {
            return await _roomRepository.GetRoomById(roomId);
        }
    }
}
