using ItemRazorV1.Models;
using ItemRazorV1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItemRazorV1.Pages.Order
{
    public class OrderItemModel : PageModel
    {
        private IItemService _itemService;
        private UserService _userService;
        private OrderService _orderService;


        public Models.Item Item { get; set; }

        public User User { get; set; }

        public Models.Order Order { get; set; } = new Models.Order();


        [BindProperty] 
        public int Count { get; set; }


        public OrderItemModel(IItemService itemService, UserService userService, OrderService orderService)
        {
            _itemService = itemService;
            _userService = userService;
            _orderService = orderService;
        }


        public void OnGet(int id)
        {
            Item = _itemService.GetItem(id);
            User = _userService.GetUserByUserName(HttpContext.User.Identity.Name);
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Item = _itemService.GetItem(id);
            User = _userService.GetUserByUserName(HttpContext.User.Identity.Name);
            Order.UserId = User.UserId;
            Order.ItemId = Item.Id;
            Order.Date = DateTime.Now;
            Order.Count = Count;
            await _orderService.AddOrderAsync(Order);
            return RedirectToPage("../Item/GetAllItems");
        }
    }
}
