namespace RestaurantService.Interfaces
{
    public class OrderData
    {
        public string Dish { get; set; }
        public decimal Price { get; set; }
        public OrderState State { get; set; }
    }
}