using Microsoft.AspNetCore.SignalR;

namespace LuuTrieuViRazorPage.Hubs
{
    public class BookingHub : Hub
    {
        public async Task BookingRoom(DateTime startTime, DateTime endTime)
        {
            await Clients.All.SendAsync("RenderRoomList", startTime, endTime);
        }
    }
}
