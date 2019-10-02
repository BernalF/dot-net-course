using System;
using System.Threading.Tasks;
using BookStore.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.WebClient.Pages.Books
{
    public class CreateModel : PageModel
    {
        [BindProperty] public Entities.Models.Books Books { get; set; }

        private readonly ApiClient _apiClient;

        public CreateModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IActionResult OnGet()
        {
            ViewData["Authors"] = new SelectList(_apiClient.GetAllAsync<Authors>(_apiClient.AuthorsApiMethod).Result, "Id", "FullName");
            ViewData["Categories"] = new SelectList(_apiClient.GetAllAsync<Entities.Models.Categories>(_apiClient.CategoriesApiMethod).Result, "Id", "Name");
        
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Books.InsertDate = DateTime.Now;

            await _apiClient.CreateAsync(_apiClient.BooksApiMethod, Books);

            return RedirectToPage("./Index");
        }
    }
}