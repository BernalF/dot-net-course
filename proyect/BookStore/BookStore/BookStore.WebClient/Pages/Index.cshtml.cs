using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.WebClient.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<Entities.Models.Books> Books { get; set; }

        [BindProperty]
        public IList<Entities.Models.Categories> Categories { get; set; }

        [BindProperty]
        public IList<Entities.Models.Reviews> Reviews { get; set; }

        private readonly ApiClient _apiClient;
        public IndexModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task OnGetAsync()
        {
            Books = await _apiClient.GetAllAsync<Entities.Models.Books>(_apiClient.BooksTopApiMethod);

            Categories = await _apiClient.GetAllAsync<Entities.Models.Categories>(_apiClient.CategoriesTopApiMethod);

            Reviews = await _apiClient.GetAllAsync<Entities.Models.Reviews>(_apiClient.ReviewsTopApiMethod);
        }
    }
}
