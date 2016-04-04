using EStore.BL.Models._Common;

namespace EStore.BL.Models.Product
{
    public class ProductFeedbackItem : ViewModelBase
    {
        public long Id { get; set; }
        public string Pluses { get; set; }
        public string Minuses { get; set; }
        public string Comment { get; set; }
        public int? Stars { get; set; }
        public string UserName { get; set; }
        public long? ProductId { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Pluses: {Pluses}, Minuses: {Minuses}, Comment: {Comment}, Stars: {Stars}, UserName: {UserName}, ProductId: {ProductId}";
        }
    }
}