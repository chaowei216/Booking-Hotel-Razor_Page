using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.Repository.Interface;

namespace DataAccess.Repository
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        public async Task<RoomType> AddRoomType(RoomType roomType)
        {
            return await RoomTypeDAO.Instance.AddRoomType(roomType);
        }

        public bool DeleteRoomType(int id)
        {
            return RoomTypeDAO.Instance.DeleteRoomType(id);
        }

        public async Task<IEnumerable<RoomType>> GetAllRoomTypes()
        {
            return await RoomTypeDAO.Instance.GetAll();
        }

        public async Task<RoomType?> GetRoomTypeById(int id)
        {
            return await RoomTypeDAO.Instance.GetRoomType(id);
        }

        public RoomType UpdateRoomType(RoomType roomType)
        {
            return RoomTypeDAO.Instance.UpdateRoomType(roomType);
        }
    }
}
