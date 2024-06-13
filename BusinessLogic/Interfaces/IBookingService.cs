using BusinessObject.Models;
using Common.DTO;
using DataAccess.DAO;

namespace BusinessLogic.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingReservation>> GetBookingsOfCustomer(int customerId);
        Task<IEnumerable<BookingDetail>> GetBookingDetailsOfBooking(int bookingId);
        Task<IEnumerable<BookingDetail>> GetAllBookingDetails();
        Task AddNewBooking(BookingReservation booking, List<BookingDetailDTO> bookingDetails);
    }
}
