using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.WebClient.Pages.Books
{
    public class DetailsModel : PageModel
    {
        [BindProperty] public Entities.Models.Books Books { get; set; }

        private readonly ApiClient _apiClient;

        public DetailsModel(ApiClient apiClient)
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
    }
}