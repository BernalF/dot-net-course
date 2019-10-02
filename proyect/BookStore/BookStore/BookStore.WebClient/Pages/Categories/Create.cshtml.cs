using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.WebClient.Pages.Categories
{
    public class CreateModel : PageModel
    {
        [BindProperty] public Entities.Models.Categories Categories { get; set; }

        private readonly ApiClient _apiClient;

        public CreateModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

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

            Categories.InsertDate = DateTime.Now;

            await _apiClient.CreateAsync(_apiClient.CategoriesApiMethod, Categories);

            return RedirectToPage("./Index");
        }
    }
}