using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItemRazorV1.Pages.LogIn
{
    public class LogOutPageModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            //LogInPageModel.LoggedInUser = null;

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/index");
        }
    }
}
