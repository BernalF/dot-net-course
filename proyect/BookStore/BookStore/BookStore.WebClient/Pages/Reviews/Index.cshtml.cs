using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.WebClient.Pages.Reviews
{
    public class IndexModel : PageModel
    {
        private readonly ApiClient _apiClient;

        public IList<Entities.Models.Reviews> Reviews { get; set; }

        public IndexModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        
        public async Task OnGetAsync()
        {
            Reviews = await _apiClient.GetAllAsync<Entities.Models.Reviews>(_apiClient.ReviewsApiMethod);
        }
    }
}
