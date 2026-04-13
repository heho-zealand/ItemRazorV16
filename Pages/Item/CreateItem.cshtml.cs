using ItemRazorV1.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItemRazorV1.Pages.Item
{
    [Authorize(Roles = "admin")]
    public class CreateItemModel : PageModel
    {
        private IItemService _itemService;
		private IWebHostEnvironment _webHostEnvironment;

		public CreateItemModel(IItemService itemService, IWebHostEnvironment webHost)
        {
            _itemService = itemService;
			_webHostEnvironment = webHost;
		}

        [BindProperty]
        public Models.Item Item { get; set; }

		[BindProperty]
		public IFormFile Photo { get; set; }

		public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
			if (!ModelState.IsValid)
			{
				return Page();
			}
			if (Photo != null)
			{
				if (Item.ItemImage != null)
				{
					string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/images", Item.ItemImage);
					System.IO.File.Delete(filePath);
				}

				Item.ItemImage = ProcessUploadedFile();
			}
			await _itemService.AddItemAsync(Item);
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
