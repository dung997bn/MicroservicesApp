using AspnetRunBasics.ApiCollection.Interfaces;
using AspnetRunBasics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace AspnetRunBasics
{
    public class ProductDetailModel : PageModel
    {
        private readonly ICatalogApi _catalogApi;
        private readonly IBasketApi _basketApi;

        public ProductDetailModel(ICatalogApi catalogApi, IBasketApi basketApi)
        {
            _catalogApi = catalogApi ?? throw new ArgumentNullException(nameof(catalogApi));
            _basketApi = basketApi ?? throw new ArgumentNullException(nameof(basketApi));
        }

        public CatalogModel Catalog { get; set; }

        [BindProperty]
        public string Color { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGetAsync(string catalogId)
        {
            if (catalogId == null)
            {
                return NotFound();
            }

            Catalog = await _catalogApi.GetCatalog(catalogId);
            if (Catalog == null)
            {
                return NotFound();
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