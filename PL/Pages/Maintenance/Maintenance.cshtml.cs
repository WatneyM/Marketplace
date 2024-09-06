using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DAL.Enums;

using DSL.Adapters.Maintenance.Order;
using DSL.Adapters.Maintenance.Product;
using DSL.Services.Declarations;

namespace PL.Pages.Maintenance
{
    [Authorize(Roles = "Administrator")]
    public class MaintenanceModel(IOrderService orderService,
        IProductService productService,
        IAuthService authService,
        ISupplyService supplyService) : PageModel
    {
        private readonly IOrderService _orderService = orderService;
        private readonly IProductService _productService = productService;
        private readonly IAuthService _authService = authService;
        private readonly ISupplyService _supplyService = supplyService;

        public List<OrderRAdapter> Orders { get; set; } = [];
        public List<ShortOrderAdapter> Products { get; set; } = [];

        public async Task OnGet(OrderProcessingState os)
        {
            Orders.AddRange(_orderService.GetOrders(os));
            Products.AddRange(_productService.GetProducts(Orders.Select(s => s.AttachedProduct)!));

            foreach (OrderRAdapter order in Orders)
            {
                order.AttachedUsername = await _authService.GetFullname(order.AttachedUsername);
            }
        }

        public IActionResult OnPostNextState(string key, OrderProcessingState state)
        {
            _orderService.NextState(key, ++state);

            return Redirect($"/maintenance/{(int)state}");
        }

        public IActionResult OnPostComplete(string key, string url)
        {
            OrderRAdapter order = _orderService.GetOrder(key);

            if (_orderService.StoreOrder(key, OrderCompletionState.Completed))
            {
                _orderService.RemoveOrder(key);
            }

            return Redirect(url);
        }

        public IActionResult OnPostRevoke(string key, string url)
        {
            OrderRAdapter order = _orderService.GetOrder(key);

            if (_supplyService.RestoreAmount(order.AttachedProduct, order.Amount))
            {
                _orderService.StoreOrder(key, OrderCompletionState.Revoked);
                _orderService.RemoveOrder(key);
            }

            return Redirect(url);
        }
    }
}