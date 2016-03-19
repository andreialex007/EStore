using EStore.BL.Models._Common;

namespace EStore.BL.Models.Product
{
    public class ProductSingleItem : ViewModelBase
    {
        public long Id { get; set; }
        public decimal? BuyPrice { get; set; }
        public decimal? SellPrice { get; set; }
        public long? IsNew { get; set; }
        public long? ProductId { get; set; }
        public bool? IsSelling { get; set; }
        public long? OrderId { get; set; }
    }
}