using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EStore.BL.Models._Common;
using EStore.BL.Utils;

namespace EStore.BL.Models.Product
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

        public List<FileItem> ProductImages { get; set; } = new List<FileItem>();
        public List<ProductSingleItem> ProductSingleItems { get; set; } = new List<ProductSingleItem>();

    }
}