using ItemRazorV1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItemRazorV1.Pages.Item
{
    public class DetailItemModel : PageModel
    {
        [BindProperty]
        public Models.Item Item { get; set; }

        private IItemService _itemService;

        public DetailItemModel(IItemService itemService)
        {
            _itemService = itemService;
        }

        
        public IActionResult OnGet(int id)
        {
            Item = _itemService.GetItem(id);
            if (Item == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return Page();
        }
    }
}
