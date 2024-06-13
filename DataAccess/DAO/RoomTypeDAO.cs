using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class RoomTypeDAO
    {
        private static RoomTypeDAO? instance;
        private static readonly object instanceLock = new object();

        public RoomTypeDAO()
        {

        }

        public static RoomTypeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoomTypeDAO();
                    }
                }
                return instance;
            }
        }

        public async Task<IEnumerable<RoomType>> GetAll()
        {
            using var db = new FuminiHotelManagementContext();
            return await db.RoomTypes.ToListAsync();
        }

        public Task<RoomType?> GetRoomType(int id)
        {
            using var db = new FuminiHotelManagementContext();
            return db.RoomTypes.FirstOrDefaultAsync(c => c.RoomTypeId == id);
        }

        public async Task<RoomType> AddRoomType(RoomType roomType)
        {
            using var db = new FuminiHotelManagementContext();
            await db.RoomTypes.AddAsync(roomType);
            await db.SaveChangesAsync();
            return roomType;
        }

        public bool DeleteRoomType(int id)
        {
            using var db = new FuminiHotelManagementContext();
            var roomType = db.RoomTypes.FirstOrDefault(c => c.RoomTypeId == id);
            if (roomType != null)
            {
                db.RoomTypes.Remove(roomType);
                return db.SaveChanges() > 0;
            }

            return false;
        }

        public RoomType UpdateRoomType(RoomType roomType)
        {
            using var db = new FuminiHotelManagementContext();
            db.RoomTypes.Update(roomType);
            db.SaveChanges();
            return roomType;
        }
    }
}
