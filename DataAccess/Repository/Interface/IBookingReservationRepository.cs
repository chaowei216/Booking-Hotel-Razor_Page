using BusinessObject.Models;

namespace DataAccess.Repository.Interface
{
    public interface IBookingReservationRepository
    {
        Task<IEnumerable<BookingReservation>> GetAllBookings();
        Task<BookingReservation?> GetBookingById(int id);
        Task<BookingReservation> AddNewBooking(BookingReservation bookingReservation);
        Task<IEnumerable<BookingDetail>> GetAllBookingDetails();
        Task<IEnumerable<BookingDetail>> GetBookingDetailsByBookingId(int id);
        Task<IEnumerable<BookingDetail>> GetBookingDetailsByRoomId(int id);
        Task<BookingDetail> AddNewBookingDetail(BookingDetail bookingDetail);
        Task<IEnumerable<BookingReservation>> GetBookingsByCustomerId(int customerId);
    }
}
