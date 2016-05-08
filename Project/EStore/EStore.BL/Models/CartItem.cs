namespace EStore.BL.Models
{
    public class CartItem
    {
        public long ProductId { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public decimal Total => Price * Count;
    }
}