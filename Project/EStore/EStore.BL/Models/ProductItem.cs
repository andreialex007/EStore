using System.ComponentModel.DataAnnotations;
using EStore.BL.Models._Common;
using EStore.BL.Utils;

namespace EStore.BL.Models
{
    public class ProductItem : ViewModelBase
    {
        public long Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

        public string Descripton { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string DescriptionText => CommonUtils.StripHtml(Descripton).Trim();
    }
}