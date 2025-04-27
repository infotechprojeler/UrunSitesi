using Microsoft.AspNetCore.Mvc;
using UrunSitesi.Core.Entities;

namespace UrunSitesi.WebAPIUsing.ViewComponents
{
    public class Categories : ViewComponent
    {
        string _apiAdres = "https://localhost:7279/api/Categories/";
        private readonly HttpClient _httpClient;

        public Categories(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres);
            return View(model);
        }
    }
}
