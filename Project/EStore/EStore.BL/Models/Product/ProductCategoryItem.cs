using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EStore.BL.Models._Common;

namespace EStore.BL.Models.Product
{
    public class ProductCategoryItem : ViewModelBase
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public long? ParentCategoryId { get; set; }
        public string ParentCategoryName { get; set; }

        public List<ProductCategoryItem> ChildCategories { get; set; } = new List<ProductCategoryItem>();
        public List<ProductCategoryItem> AvaliableCategories { get; set; } = new List<ProductCategoryItem>();
    }
}