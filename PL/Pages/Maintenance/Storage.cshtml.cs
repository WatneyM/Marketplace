using Microsoft.AspNetCore.Mvc.RazorPages;

using DAL.Enums;

using DSL.Adapters.Maintenance.Order;
using DSL.Adapters.Maintenance.Product;
using DSL.Services.Declarations;

namespace PL.Pages.Maintenance
{
    public class StorageModel(IOrderService orderService,
        IProductService productService,
        IAuthService authService) : PageModel
    {
        private readonly IOrderService _orderService = orderService;
        private readonly IProductService _productService = productService;
        private readonly IAuthService _authService = authService;

        public List<StoredOrderAdapter> Orders { get; set; } = [];
        public List<ShortOrderAdapter> Products { get; set; } = [];

        public async Task OnGet(OrderCompletionState os)
        {
            Orders.AddRange(_orderService.GetStored(os));
            Products.AddRange(_productService.GetProducts(Orders.Select(s => s.AttachedProduct)!));

            foreach (StoredOrderAdapter order in Orders)
            {
                order.AttachedUsername = await _authService.GetFullname(order.AttachedUsername);
            }
        }
    }
}
