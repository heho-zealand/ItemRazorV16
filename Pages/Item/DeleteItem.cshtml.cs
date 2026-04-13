using ItemRazorV1.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItemRazorV1.Pages.Item
{
    [Authorize(Roles = "admin")]
    public class DeleteItemModel : PageModel
    {
        private IItemService _itemService;

        public DeleteItemModel(IItemService itemService)
        {
            _itemService = itemService;
        }

        [BindProperty]
        public Models.Item Item { get; set; }


        public IActionResult OnGet(int id)
        {
            Item = _itemService.GetItem(id);
            if (Item == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Models.Item deletedItem = await _itemService.DeleteItemAsync(Item.Id);
            if (deletedItem == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return RedirectToPage("GetAllItems");
        }
    }
}
