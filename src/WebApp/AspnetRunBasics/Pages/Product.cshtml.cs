using AspnetRunBasics.ApiCollection.Interfaces;
using AspnetRunBasics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetRunBasics
{
    public class ProductModel : PageModel
    {
        private readonly ICatalogApi _catalogApi;
        private readonly IBasketApi _basketApi;

        public ProductModel(ICatalogApi catalogApi, IBasketApi basketApi)
        {
            _catalogApi = catalogApi;
            _basketApi = basketApi;
        }

        public IEnumerable<string> CategoryList { get; set; } = new List<string>();
        public IEnumerable<CatalogModel> CatalogList { get; set; } = new List<CatalogModel>();


        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(string categoryName)
        {
            var catalogList = await _catalogApi.GetCatalog();

            CategoryList = catalogList.Select(p => p.Category).Distinct();

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                CatalogList = catalogList.Where(p => p.Category == categoryName);
                SelectedCategory = categoryName;
            }
            else
            {
                CatalogList = catalogList;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string catalogId)
        {
            var catalog = await _catalogApi.GetCatalog(catalogId);
            var userName = "dn";
            var basket = await _basketApi.GetBasket(userName);
            basket.Items.Add(new BasketItemModel
            {
                ProductId = catalogId,
                ProductName = catalog.Name,
                Price = catalog.Price,
                Quantity = 1,
                Color = "Black"
            });
            var basketUpdated = await _basketApi.UpdateBasket(basket);
            return RedirectToPage("Cart");
        }
    }
}