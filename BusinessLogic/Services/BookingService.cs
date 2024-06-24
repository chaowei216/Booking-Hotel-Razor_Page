using BusinessLogic.Interfaces;
using BusinessObject.Models;
using Common.DTO;
using DataAccess.Repository.Interface;

namespace BusinessLogic.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingReservationRepository _reservationRepository;
        private readonly IRoomService _roomService;

        public BookingService(IBookingReservationRepository reservationRepository, IRoomService roomService)
        {
            _reservationRepository = reservationRepository;
            _roomService = roomService;
        }

        public async Task AddNewBooking(BookingReservation booking, List<BookingDetailDTO> bookingDetails)
        {
            var allBooking = await _reservationRepository.GetAllBookings();
            if (!allBooking.Any())
            {
                booking.BookingReservationId = 1;
            } else
            {
                booking.BookingReservationId = allBooking.Last().BookingReservationId + 1;
            }
            var addedBooking = await _reservationRepository.AddNewBooking(booking);
            foreach (var item in bookingDetails)
            {
                DateOnly startDateOnly = new DateOnly(item.SDate.Year, item.SDate.Month, item.SDate.Day);
                DateOnly endDateOnly = new DateOnly(item.EDate.Year, item.EDate.Month, item.EDate.Day);
                var roomAvailable = await _roomService.GetAvailableRoomsByTime(item.SDate, item.EDate);
                if(roomAvailable != null && roomAvailable.Any(p => p.RoomId == item.Id))
                {
                    await _reservationRepository.AddNewBookingDetail(new BookingDetail()
                    {
                        RoomId = item.Id,
                        ActualPrice = (decimal)item.Price,
                        BookingReservationId = addedBooking.BookingReservationId,
                        StartDate = startDateOnly,
                        EndDate = endDateOnly,
                    });

                    if (startDateOnly == DateOnly.FromDateTime(DateTime.Now))
                    {
                        var room = await _roomService.GetRoom(item.Id);
                        if (room != null)
                        {
                            room.RoomStatus = 0;
                            _roomService.UpdateRoom(room);
                        }
                    }
                }

            }
        }

        public async Task<IEnumerable<BookingReservation>> GetAllBooking()
        {
            return await _reservationRepository.GetAllBookings();
        }

        public async Task<IEnumerable<BookingDetail>> GetAllBookingDetails()
        {
            return await _reservationRepository.GetAllBookingDetails();
        }

        public async Task<IEnumerable<BookingDetail>> GetBookingDetailsOfBooking(int bookingId)
        {
            return await _reservationRepository.GetBookingDetailsByBookingId(bookingId);
        }

        public async Task<IEnumerable<BookingReservation>> GetBookingsOfCustomer(int customerId)
        {
            return await _reservationRepository.GetBookingsByCustomerId(customerId);
        }
    }
}
