using EStore.BL.Models._Common;

namespace EStore.BL.Models.SupplierInvoice
{
    public class SupplierInvoicePositionItem : ViewModelBase
    {
        public long Id { get; set; }
        public long? ProductId { get; set; }
        public string ProductName { get; set; }
        public int? Qty { get; set; }
        public decimal? Price { get; set; }
        public string Note { get; set; }
        public long? SupplierInvoiceId { get; set; }
        public decimal Total => (Qty ?? 0) * (Price ?? 0);
    }
}