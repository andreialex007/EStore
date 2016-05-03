using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EStore.BL.Models._Common;
using EStore.BL.Utils;
using EStore.DL.Mapping;

namespace EStore.BL.Models.Product
{
    public class ProductItem : ViewModelBase
    {
        public long Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

        public string Descripton { get; set; }

        public long? SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }

        public string Specs { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string DescriptionText => CommonUtils.StripHtml(Descripton).Trim();

        public long? CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string MainImage { get; set; }
        public bool IsAvaliable { get; set; }
        public decimal Price { get; set; }

        public List<ProductCategoryItem> AvaliableCategories { get; set; } = new List<ProductCategoryItem>();
        public List<FileItem> ProductImages { get; set; } = new List<FileItem>();
        public List<ProductSingleItem> ProductSingleItems { get; set; } = new List<ProductSingleItem>();
        public List<ProductFeedbackItem> FeedbackItems { get; set; } = new List<ProductFeedbackItem>();
    }
}