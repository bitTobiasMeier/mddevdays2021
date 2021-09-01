using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Actors.Runtime;
using RestaurantService.Interfaces;

namespace RestaurantService
{
    public class OrderActor : Actor, IOrderActor, IRemindable
    {
        private const string Statekey = "mydata";

        public OrderActor(ActorHost host) : base(host)
        {
        }

        public async Task PlaceOrderAsync(OrderData order)
        {
            await this.StateManager.SetStateAsync(Statekey, order);
            await this.RegisterReminderAsync("Cooking", null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
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

        public async Task ReceiveReminderAsync(string reminderName, byte[] state, TimeSpan dueTime, TimeSpan period)
        {
            if (reminderName == "Cooking")
            {
                var order = await this.StateManager.GetStateAsync<OrderData>(Statekey);
                OrderState? nextState = null;
                switch (order.State)
                {
                    case OrderState.Ordered:
                        nextState = OrderState.Cooking;
                        break;
                    case OrderState.Cooking:
                        nextState = OrderState.Cooked;
                        break;
                    case OrderState.Cooked:
                        nextState = OrderState.Delivering;
                        break;
                    case OrderState.Delivering:
                        nextState = OrderState.Delivered;
                        break;
                }

                if (nextState != null)
                {
                    order.State = nextState.Value;
                    await this.StateManager.SetStateAsync(Statekey, order).ConfigureAwait(false);
                    if (nextState == OrderState.Delivered)
                    {
                        await this.UnregisterReminderAsync("Cooking");
                    }
                }
            }
        }
    }
}
