using EStore.BL.Models._Common;

namespace EStore.BL.Models.Product
{
    public class ProductSingleItem : ViewModelBase
    {
        public long Id { get; set; }
        public decimal? BuyPrice { get; set; }
        public decimal? SellPrice { get; set; }
        public decimal? Margin => (BuyPrice / SellPrice) * 100;
        public bool? IsNew { get; set; }
        public long? ProductId { get; set; }
        public long? OrderId { get; set; }
        public long? SupplierInvoicePositionId { get; set; }
        public long? SupplierInvoiceId { get; set; }
        public int? StateId { get; set; }
        public string Notes { get; set; }
        public ProductSingleStateEnum? StateEnum => StateId?.CastTo<ProductSingleStateEnum>();
    }

    public class ProductSingleSearchItem : ViewModelBase
    {
        public long Id { get; set; }
        public decimal? BuyPrice { get; set; }
        public decimal? SellPrice { get; set; }
        public decimal? Margin { get; set; }
        public bool? IsNew { get; set; }
        public long? ProductId { get; set; }
        public long? OrderId { get; set; }
        public long? SupplierInvoicePositionId { get; set; }
        public long? SupplierInvoiceId { get; set; }
        public string StateName { get; set; }
        public long? StateId { get; set; }
        public string Notes { get; set; }

        public string View { get; set; }
    }
}