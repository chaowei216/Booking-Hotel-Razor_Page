using BusinessObject.Models;

namespace DataAccess.Repository.Interface
{
    public interface IRoomTypeRepository
    {
        Task<IEnumerable<RoomType>> GetAllRoomTypes();
        Task<RoomType?> GetRoomTypeById(int id);
        Task<RoomType> AddRoomType(RoomType roomType);
        bool DeleteRoomType(int id);
        RoomType UpdateRoomType(RoomType roomType);
    }
}
