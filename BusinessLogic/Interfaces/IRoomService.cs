using BusinessObject.Models;

namespace BusinessLogic.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomInformation>> GetAllRooms();
        Task<RoomInformation?> GetRoom(int roomId);
        Task<IEnumerable<RoomInformation>> GetAvailableRoomsByTime(DateTime startDate, DateTime endDate);
        RoomInformation UpdateRoom(RoomInformation roomInformation);
    }
}
