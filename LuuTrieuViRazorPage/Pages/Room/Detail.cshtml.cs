using BusinessLogic.Interfaces;
using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LuuTrieuViRazorPage.Pages.Room
{
    [Authorize]
    public class DetailModel : PageModel
    {
        private readonly IRoomService _roomService;

        public DetailModel(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public RoomInformation? Room { get; set; }

        public async Task OnGetAsync(int? id)
        {
            Room = await _roomService.GetRoom((int)id!);
        }
    }
}
