using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.Repository.Interface;

namespace DataAccess.Repository
{
    public class RoomRepository : IRoomRepository
    {
        public async Task<RoomInformation> AddNewRoom(RoomInformation roomInformation)
        {
            return await RoomInformationDAO.Instance.AddRoomInformation(roomInformation);
        }

        public bool DeleteRoom(int id)
        {
            return RoomInformationDAO.Instance.DeleteRoomInformation(id);
        }

        public async Task<IEnumerable<RoomInformation>> GetAllRooms()
        {
            return await RoomInformationDAO.Instance.GetAll();
        }

        public async Task<RoomInformation?> GetRoomById(int id)
        {
            return await RoomInformationDAO.Instance.GetRoomInformation(id);
        }

        public RoomInformation UpdateRoom(RoomInformation roomInformation)
        {
            return RoomInformationDAO.Instance.UpdateRoomInformation(roomInformation);
        }
    }
}
