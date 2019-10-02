using System.Threading.Tasks;
using BookStore.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStore.WebClient.Pages.Books
{
    public class DeleteModel : PageModel
    {
        [BindProperty] public Entities.Models.Books Books { get; set; }

        private readonly ApiClient _apiClient;

        public DeleteModel(ApiClient apiClient)
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

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _apiClient.DeleteAsync(_apiClient.BooksApiMethod, id);

            return RedirectToPage("./Index");
        }
    }
}
