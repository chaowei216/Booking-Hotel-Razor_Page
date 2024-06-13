using BusinessLogic.Interfaces;
using BusinessObject.Models;
using Common.DTO;
using DataAccess.Repository.Interface;

namespace BusinessLogic.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingReservationRepository _reservationRepository;

        public BookingService(IBookingReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task AddNewBooking(BookingReservation booking, List<BookingDetailDTO> bookingDetails)
        {
            var allBooking = await _reservationRepository.GetAllBookings();
            booking.BookingReservationId = allBooking.Last().BookingReservationId + 1;
            var addedBooking = await _reservationRepository.AddNewBooking(booking);
            foreach (var item in bookingDetails)
            {
                DateOnly startDateOnly = new DateOnly(item.SDate.Year, item.SDate.Month, item.SDate.Day);
                DateOnly endDateOnly = new DateOnly(item.EDate.Year, item.EDate.Month, item.EDate.Day);
                await _reservationRepository.AddNewBookingDetail(new BookingDetail()
                {
                    RoomId = item.Id,
                    ActualPrice = (decimal)item.Price,
                    BookingReservationId = addedBooking.BookingReservationId,
                    StartDate = startDateOnly,
                    EndDate = endDateOnly
                });
            }
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
