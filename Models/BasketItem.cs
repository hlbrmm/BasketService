namespace BasketService.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
