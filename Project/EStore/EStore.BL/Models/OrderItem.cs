namespace EStore.BL.Models
{
    public class OrderItem : CheckOutModel
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public int? State { get; set; }
    }
}