using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class BookingReservationDAO
    {
        private static BookingReservationDAO? instance;
        private static readonly object instanceLock = new object();

        public BookingReservationDAO()
        {

        }

        public static BookingReservationDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookingReservationDAO();
                    }
                }
                return instance;
            }
        }

        public async Task<IEnumerable<BookingReservation>> GetAll()
        {
            using var db = new FuminiHotelManagementContext();
            return await db.BookingReservations.ToListAsync();
        }

        public Task<BookingReservation?> GetBookingReservation(int id)
        {
            using var db = new FuminiHotelManagementContext();
            return db.BookingReservations.FirstOrDefaultAsync(c => c.BookingReservationId == id);
        }

        public async Task<BookingReservation> AddBookingReservation(BookingReservation bookingReservation)
        {
            using var db = new FuminiHotelManagementContext();
            var entity = await db.BookingReservations.AddAsync(bookingReservation);
            await db.SaveChangesAsync();
            return entity.Entity;

        }

        public bool DeleteBookingReservation(int id)
        {
            using var db = new FuminiHotelManagementContext();
            var bookingReservation = db.BookingReservations.FirstOrDefault(c => c.BookingReservationId == id);
            if (bookingReservation != null)
            {
                db.BookingReservations.Remove(bookingReservation);
                return db.SaveChanges() > 0;
            }

            return false;
        }

        public BookingReservation UpdateBookingReservation(BookingReservation bookingReservation)
        {
            using var db = new FuminiHotelManagementContext();
            db.BookingReservations.Update(bookingReservation);
            db.SaveChanges();
            return bookingReservation;
        }
    }
}
