using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Actors.Runtime;
using RestaurantService.Interfaces;

namespace RestaurantService
{
    public class OrderActor : Actor, IOrderActor
    {
        private const string Statekey = "mydata";

        public OrderActor(ActorHost host) : base(host)
        {
        }

        public async Task PlaceOrderAsync(OrderData order)
        {
            await this.StateManager.SetStateAsync(Statekey, order);
        }

        public async Task<OrderData> GetOrderAsync()
        {
            var val = await this.StateManager.TryGetStateAsync<OrderData>(Statekey);
            if (val.HasValue)
            {
                return val.Value;
            }

            return new OrderData();
        }
    }
}
