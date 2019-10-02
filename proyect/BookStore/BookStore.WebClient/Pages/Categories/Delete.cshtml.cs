using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.WebClient.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        [BindProperty] public Entities.Models.Categories Categories { get; set; }

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

            Categories = await _apiClient.GetByIdAsync<Entities.Models.Categories>(_apiClient.CategoriesApiMethod, id);

            if (Categories == null)
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

            await _apiClient.DeleteAsync(_apiClient.CategoriesApiMethod, id);


            return RedirectToPage("./Index");
        }
    }
}