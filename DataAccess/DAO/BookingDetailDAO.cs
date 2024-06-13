using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class BookingDetailDAO
    {
        private static BookingDetailDAO? instance;
        private static readonly object instanceLock = new object();

        public BookingDetailDAO()
        {

        }

        public static BookingDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookingDetailDAO();
                    }
                }
                return instance;
            }
        }

        public async Task<IEnumerable<BookingDetail>> GetAll()
        {
            using var db = new FuminiHotelManagementContext();
            return await db.BookingDetails.Include(p => p.Room).Include(p => p.BookingReservation).ToListAsync();
        }

        public async Task<BookingDetail> AddBookingDetail(BookingDetail bookingDetail)
        {
            using var db = new FuminiHotelManagementContext();
            await db.BookingDetails.AddAsync(bookingDetail);
            await db.SaveChangesAsync();
            return bookingDetail;
        }

        public BookingDetail UpdateBookingDetail(BookingDetail bookingDetail)
        {
            using var db = new FuminiHotelManagementContext();
            db.BookingDetails.Update(bookingDetail);
            db.SaveChanges();
            return bookingDetail;
        }
    }
}
