using BusinessLogic.Interfaces;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace LuuTrieuViRazorPage.Pages.Admin.Booking
{
    public class IndexModel : PageModel
    {
        private readonly IBookingService _bookingService;

        public IndexModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public IEnumerable<BookingReservation> Bookings { get; set; } = new List<BookingReservation>();

        public async Task OnGet()
        {
            Bookings = await _bookingService.GetAllBooking();
        }
    }
}
