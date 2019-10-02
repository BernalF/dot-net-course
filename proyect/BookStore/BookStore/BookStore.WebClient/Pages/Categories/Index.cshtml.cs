using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.WebClient.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApiClient _apiClient;

        public IList<Entities.Models.Categories> Categories { get; set; }

        public IndexModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        
        public async Task OnGetAsync()
        {
            Categories = await _apiClient.GetAllAsync<Entities.Models.Categories>(_apiClient.CategoriesApiMethod);
        }
    }
}
