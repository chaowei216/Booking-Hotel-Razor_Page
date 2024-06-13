using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class RoomInformationDAO
    {
        private static RoomInformationDAO? instance;
        private static readonly object instanceLock = new object();

        public RoomInformationDAO()
        {

        }

        public static RoomInformationDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoomInformationDAO();
                    }
                }
                return instance;
            }
        }

        public async Task<IEnumerable<RoomInformation>> GetAll()
        {
            using var db = new FuminiHotelManagementContext();
            return await db.RoomInformations.ToListAsync();
        }

        public async Task<RoomInformation?> GetRoomInformation(int id)
        {
            using var db = new FuminiHotelManagementContext();
            return await db.RoomInformations.Include(p => p.RoomType).FirstOrDefaultAsync(c => c.RoomId == id);
        }

        public async Task<RoomInformation> AddRoomInformation(RoomInformation roomInformation)
        {
            using var db = new FuminiHotelManagementContext();
            await db.RoomInformations.AddAsync(roomInformation);
            await db.SaveChangesAsync();
            return roomInformation;
        }

        public bool DeleteRoomInformation(int id)
        {
            using var db = new FuminiHotelManagementContext();
            var room = db.RoomInformations.FirstOrDefault(c => c.RoomId == id);
            if (room != null)
            {
                db.RoomInformations.Remove(room);
                return db.SaveChanges() > 0;
            }

            return false;
        }

        public RoomInformation UpdateRoomInformation(RoomInformation roomInformation)
        {
            using var db = new FuminiHotelManagementContext();
            db.RoomInformations.Update(roomInformation);
            db.SaveChanges();
            return roomInformation;
        }
    }
}
