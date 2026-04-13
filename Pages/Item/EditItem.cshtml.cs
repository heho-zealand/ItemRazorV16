using ItemRazorV1.Service;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItemRazorV1.Pages.Item
{
    [Authorize(Roles = "admin")]
    public class EditItemModel : PageModel
    {
        private IItemService _itemService;
		private IWebHostEnvironment _webHostEnvironment;

		public EditItemModel(IItemService itemService, IWebHostEnvironment webHost)
        {
            _itemService = itemService;
			_webHostEnvironment = webHost;
		}

        [BindProperty]
        public Models.Item? Item { get; set; }

		[BindProperty]
		public IFormFile? Photo { get; set; }

		public IActionResult OnGet(int id)
        {
            Item = _itemService.GetItem(id);
            if (Item == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
			if (!ModelState.IsValid)
			{
				return Page();
			}
			if (Photo != null && Item != null)
			{
				if (Item.ItemImage != null)
				{
					string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/images", Item.ItemImage);
					System.IO.File.Delete(filePath);
				}

				Item.ItemImage = ProcessUploadedFile();
			}
			if(Item != null) { await _itemService.UpdateItemAsync(Item); }
			
            return RedirectToPage("GetAllItems");
        }

		private string ProcessUploadedFile()
		{
			string uniqueFileName = null;
			if (Photo != null)
			{
				string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
				uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					Photo.CopyTo(fileStream);
				}
			}
			return uniqueFileName;
		}
	}
}
