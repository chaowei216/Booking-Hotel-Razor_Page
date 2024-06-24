using BusinessLogic.Interfaces;
using BusinessObject.Models;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LuuTrieuViRazorPage.Pages.Booking
{
    [Authorize]
    public class DetailModel : PageModel
    {
        private readonly IBookingService _bookingService;

        public DetailModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public IEnumerable<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

        public async Task OnGetAsync(int id)
        {
            var bookingDetails = await _bookingService.GetBookingDetailsOfBooking(id);

            if (bookingDetails != null && bookingDetails.Any())
            {
                BookingDetails = bookingDetails;
            }
        }
    }
}
