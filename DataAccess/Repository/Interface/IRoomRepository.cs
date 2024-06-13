using BusinessObject.Models;

namespace DataAccess.Repository.Interface
{
    public interface IRoomRepository
    {
        Task<IEnumerable<RoomInformation>> GetAllRooms();
        Task<RoomInformation?> GetRoomById(int id);
        Task<RoomInformation> AddNewRoom(RoomInformation roomInformation);
        bool DeleteRoom(int id);
        RoomInformation UpdateRoom(RoomInformation roomInformation);
    }
}
