using System;
using System.Threading.Tasks;
using BookStore.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.WebClient.Pages.Books
{
    public class EditModel : PageModel
    {
        [BindProperty] public Entities.Models.Books Books { get; set; }

        private readonly ApiClient _apiClient;

        public EditModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Books = await _apiClient.GetByIdAsync<Entities.Models.Books>(_apiClient.BooksApiMethod, id);

            if (Books == null)
            {
                return NotFound();
            }

            ViewData["Authors"] = new SelectList(await _apiClient.GetAllAsync<Authors>(_apiClient.AuthorsApiMethod), "Id", "FullName");
            ViewData["Categories"] = new SelectList(await _apiClient.GetAllAsync<Entities.Models.Categories>(_apiClient.CategoriesApiMethod), "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Books.InsertDate = DateTime.Now;

            await _apiClient.EditAsync(_apiClient.BooksApiMethod, Books.Id, Books);

            return RedirectToPage("./Index");
        }
    }
}