using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.Repository.Interface;

namespace DataAccess.Repository
{
    public class BookingReservationRepository : IBookingReservationRepository
    {
        public async Task<BookingReservation> AddNewBooking(BookingReservation bookingReservation)
        {
            return await BookingReservationDAO.Instance.AddBookingReservation(bookingReservation);
        }

        public async Task<BookingDetail> AddNewBookingDetail(BookingDetail bookingDetail)
        {
            return await BookingDetailDAO.Instance.AddBookingDetail(bookingDetail);
        }

        public async Task<IEnumerable<BookingDetail>> GetAllBookingDetails()
        {
            return await BookingDetailDAO.Instance.GetAll();
        }

        public async Task<IEnumerable<BookingReservation>> GetAllBookings()
        {
            return await BookingReservationDAO.Instance.GetAll();
        }

        public async Task<BookingReservation?> GetBookingById(int id)
        {
            return await BookingReservationDAO.Instance.GetBookingReservation(id);
        }

        public async Task<IEnumerable<BookingDetail>> GetBookingDetailsByBookingId(int id)
        {
            var bookingDetails = await BookingDetailDAO.Instance.GetAll();
            return bookingDetails.Where(b => b.BookingReservationId == id).ToList();
        }

        public async Task<IEnumerable<BookingDetail>> GetBookingDetailsByRoomId(int id)
        {
            var bookingDetails = await BookingDetailDAO.Instance.GetAll();
            return bookingDetails.Where(b => b.RoomId == id).ToList();
        }

        public async Task<BookingDetail?> GetBookingOfRoomByCurrentDate(int roomId)
        {
            var allBookingDetails = await BookingDetailDAO.Instance.GetAll();
            return allBookingDetails.FirstOrDefault(p => p.RoomId == roomId && 
                                                    p.StartDate <= DateOnly.FromDateTime(DateTime.Now) && 
                                                    p.EndDate >= DateOnly.FromDateTime(DateTime.Now));
        }

        public async Task<IEnumerable<BookingReservation>> GetBookingsByCustomerId(int customerId)
        {
            var bookings = await BookingReservationDAO.Instance.GetAll();
            return bookings.Where(p => p.CustomerId == customerId).ToList();
        }
    }
}
