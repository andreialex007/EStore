namespace EStore.BL.Models.Product
{
    public class ProductImageItem
    {
        public long Id { get; set; }
        public string Path { get; set; }

        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);

        public string Description { get; set; }
        public long? ProductId { get; set; }
    }
}