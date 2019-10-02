using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.WebClient.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly ApiClient _apiClient;

        public IList<Entities.Models.Books> Books { get; set; }

        public IndexModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task OnGetAsync()
        {
            Books = await _apiClient.GetAllAsync<Entities.Models.Books>(_apiClient.BooksApiMethod);
        }
    }
}
