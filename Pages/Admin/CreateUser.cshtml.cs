using ItemRazorV1.Models;
using ItemRazorV1.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ItemRazorV1.Pages.Admin
{
    [Authorize(Roles = "admin")]    
    public class CreateUserModel : PageModel

    {
        [BindProperty]
        public string UserName { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }

        private UserService _userService;

        private PasswordHasher<string> passwordHasher;

        public CreateUserModel(UserService userService)
        {
            _userService = userService;
            passwordHasher = new PasswordHasher<string>();
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _userService.AddUserAsync(new User(UserName, passwordHasher.HashPassword(null, Password)));
            return RedirectToPage("/Item/GetAllItems");
        }
    }
}
