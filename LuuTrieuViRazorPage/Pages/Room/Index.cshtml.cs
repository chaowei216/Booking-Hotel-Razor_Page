using BusinessLogic.Interfaces;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;
using BusinessLogic.Services;
using Common.DTO;
using System.Security.Claims;

namespace LuuTrieuViRazorPage.Pages.Room
{
    public class IndexModel : PageModel
    {
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;
        private readonly ICustomerService _customerService;

        public IndexModel(IRoomService roomService, IBookingService bookingService, ICustomerService customerService)
        {
            _roomService = roomService;
            _bookingService = bookingService;
            _customerService = customerService;
        }

        public IEnumerable<RoomInformation> Rooms { get; set; } = new List<RoomInformation>();
        public Customer? Customer { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return RedirectToPage("/Login");
            }

            Rooms = await _roomService.GetAvailableRoomsByTime(DateTime.Now, DateTime.Now);
            Customer = await _customerService.GetCustomerByEmail(User.FindFirstValue(ClaimTypes.Email)!);

            return Page();
        }

        public async Task<IActionResult> OnGetUpdateRoomList(DateTime startDate, DateTime endDate)
        {
            Rooms = await _roomService.GetAvailableRoomsByTime(startDate, endDate);

            if (Rooms != null && Rooms.Any())
            {
                var roomData = new List<string>();

                foreach (var room in Rooms)
                {
                    var jsonRoom = JsonConvert.SerializeObject(new
                    {
                        id = room.RoomId,
                        number = room.RoomNumber,
                        description = room.RoomDetailDescription,
                        price = room.RoomPricePerDay,
                    });

                    // add to list
                    roomData.Add(jsonRoom);
                }

                return new JsonResult(new
                {
                    isSuccess = true,
                    data = roomData
                });
            }

            return new JsonResult(new
            {
                isSuccess = false
            });
        }

        public async Task<IActionResult> OnPostBookingAsync([FromBody] BookingModel data)
        {
            BookingReservation newBooking = new BookingReservation
            {
                BookingDate = DateOnly.FromDateTime(DateTime.Today),
                BookingStatus = 1,
                CustomerId = data.CustomerId,
                TotalPrice = decimal.Parse(data.Price.ToString())
            };

            await _bookingService.AddNewBooking(newBooking, data.Details);

            Rooms = await _roomService.GetAvailableRoomsByTime(DateTime.Now, DateTime.Now);

            if (Rooms != null && Rooms.Any())
            {
                var roomData = new List<string>();

                foreach (var room in Rooms)
                {
                    var jsonRoom = JsonConvert.SerializeObject(new
                    {
                        id = room.RoomId,
                        number = room.RoomNumber,
                        description = room.RoomDetailDescription,
                        price = room.RoomPricePerDay,
                    });

                    roomData.Add(jsonRoom);
                }

                return new JsonResult(new
                {
                    isSuccess = true,
                    data = roomData
                });
            } else
            {
                return new JsonResult(new
                {
                    isSuccess = true,
                    data = new List<string>()
                }) ;
            }
        }
    }
}
