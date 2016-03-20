namespace EStore.BL.Models.Product
{
    public class FileItem
    {
        public long Id { get; set; }
        public string Path { get; set; }

        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);

        public string Description { get; set; }
        public long? ProductId { get; set; }
        public long? SupplierInvoiceId { get; set; }
    }


}