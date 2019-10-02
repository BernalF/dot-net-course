using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.WebClient.Pages.Reviews
{
    public class CreateModel : PageModel
    {
        [BindProperty] public Entities.Models.Reviews Reviews { get; set; }

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

            Reviews.InsertDate = DateTime.Now;

            await _apiClient.CreateAsync(_apiClient.ReviewsApiMethod, Reviews);

            return RedirectToPage("./Index");
        }
    }
}