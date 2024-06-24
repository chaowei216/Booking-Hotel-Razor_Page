using BusinessLogic.Interfaces;
using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace LuuTrieuViRazorPage.Pages.Booking
{
    [Authorize(Roles = "Customer")]
    public class IndexModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IBookingService _bookingService;

        public IndexModel(ICustomerService customerService, IBookingService bookingService)
        {
            _customerService = customerService;
            _bookingService = bookingService;
        }

        public IEnumerable<BookingReservation> Bookings { get; set; } = new List<BookingReservation>();

        public async Task<IActionResult> OnGet()
        {
            var customer = await _customerService.GetCustomerByEmail(User.FindFirstValue(ClaimTypes.Email)!);

            if (customer != null)
            {
                Bookings = await _bookingService.GetBookingsOfCustomer(customer.CustomerId);
            }

            return Page();
        }
    }
}
