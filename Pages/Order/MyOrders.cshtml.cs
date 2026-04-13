using ItemRazorV1.DAO;
using ItemRazorV1.Models;
using ItemRazorV1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItemRazorV1.Pages.Order
{
    public class MyOrdersModel : PageModel
    {
        public UserService UserService { get; set; }

        //public IEnumerable<OrderDAO> MyOrders { get; set; }
        public IEnumerable<Models.Order> MyOrders { get; set; }
       

        public MyOrdersModel(UserService userService)
        {
            UserService = userService;
        }
        public IActionResult OnGetAsync()
        {
            User CurrentUser = UserService.GetUserByUserName(HttpContext.User.Identity.Name);
            MyOrders = UserService.GetUserOrdersAsync(CurrentUser).Result.Orders;

            return Page();
        }
    }
}
