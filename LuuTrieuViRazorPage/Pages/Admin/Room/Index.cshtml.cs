using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LuuTrieuViRazorPage.Pages.Admin.Room
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IRoomService _roomService;

        public IndexModel(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [BindProperty]
        public IEnumerable<RoomInformation> Rooms {  get; set; } = new List<RoomInformation>();

        public async Task OnGet()
        {
            Rooms = await _roomService.GetAllRooms();
        }
    }
}
