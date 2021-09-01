using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Actors;
using Dapr.Actors.Client;
using RestaurantService.Interfaces;

namespace Platform.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostOrderAsync(string orderid, string dish, decimal price)
        {
            var order = new OrderData() { Dish = dish, Price = price, State = OrderState.Ordered };
            var factory = new ActorProxyFactory();
            var actor = factory.CreateActorProxy<IOrderActor>(new ActorId(orderid), "OrderActor");
            await actor.PlaceOrderAsync(order);
            ViewData["Dish"] = order;
            ViewData["orderid"] = orderid;
            return this.Page();
        }

        public async Task<IActionResult> OnPostRefreshAsync(string orderid)
        {
            var factory = new ActorProxyFactory();
            var actor = factory.CreateActorProxy<IOrderActor>(new ActorId(orderid), "OrderActor");
            var order = await actor.GetOrderAsync();
            ViewData["Dish"] = order;
            ViewData["orderid"] = orderid;
            return this.Page();
        }
    }
}
