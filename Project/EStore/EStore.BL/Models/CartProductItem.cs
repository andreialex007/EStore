namespace EStore.BL.Models
{
    public class CartProductItem : CartItem
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}