using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Product;
using DSL.Services.Declarations;

namespace PL.Pages.Maintenance.Product
{
    [BindProperties]
    public class ListModel(IProductService service) : PageModel
    {
        private readonly IProductService _service = service;

        public List<ProductRAdapter> Products { get; set; } = [];

        public void OnGet()
        {
            Products = _service.GetProductsAsList().ToList();
        }
    }
}