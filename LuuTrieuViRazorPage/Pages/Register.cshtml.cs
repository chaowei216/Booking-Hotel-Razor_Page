using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessObject.Models;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LuuTrieuViRazorPage.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAuthService _authService;

        public RegisterModel(IAuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public RegisterRequestDTO RegisterRequest { get; set; } = null!;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var customer = await _authService.Register(RegisterRequest);

            if (customer != null)
            {
                return RedirectToPage("Login");
            }

            return Page();
        }
    }
}
