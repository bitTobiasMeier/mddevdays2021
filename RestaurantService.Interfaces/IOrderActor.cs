using System;
using System.Threading.Tasks;
using Dapr.Actors;

namespace RestaurantService.Interfaces
{
    public interface IOrderActor : IActor
    {
        Task PlaceOrderAsync(OrderData order);
        Task<OrderData> GetOrderAsync();
    }
}
