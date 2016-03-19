namespace EStore.BL.Models.SupplierInvoice
{
    public class SupplierInvoicePositionItem
    {
        public long Id { get; set; }
        public long? ProductId { get; set; }
        public int? Qty { get; set; }
        public decimal? Price { get; set; }
        public string Note { get; set; }
        public long? SupplierInvoiceId { get; set; }
    }
}