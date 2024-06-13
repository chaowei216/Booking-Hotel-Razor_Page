using BusinessLogic.Interfaces;
using Common.DTO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Common.ViewModels;
using Common.Enum;
using Common.Constant.Message;

namespace LuuTrieuViRazorPage.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public LoginModel(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;

        }

        [BindProperty]
        public LoginRequestDTO LoginRequest  { get; set; } = null!;

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _authService.CheckLogin(LoginRequest.Email, LoginRequest.Password);

            if (user != null)
            {
                //if found, create new cookie auth for user
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.CustomerFullName!),
                    new(ClaimTypes.Email, user.EmailAddress)
                };

                // claims identity
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //auth properties
                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,

                    ExpiresUtc = DateTimeOffset.Now.AddMinutes(double.Parse(_configuration["Cookie:ExpireTime"]!)),

                    IsPersistent = true,

                    IssuedUtc = DateTime.Now,

                    RedirectUri = "/login"
                };

                //register cookie auth
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToPage("Index");
            } else
            {
                return Page();
            }
        }
    }
}
