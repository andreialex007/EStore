using EStore.BL.Models._Common;

namespace EStore.BL.Models
{
    public class ProductItem : ViewModelBase
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Descripton { get; set; }
    }
}