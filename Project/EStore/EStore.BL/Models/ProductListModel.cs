using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EStore.BL.Models.Product;

namespace EStore.BL.Models
{
    public class ProductListModel
    {
        public long? SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public long? CategoryId { get; set; }
        public string CategoryName { get; set; }

        public List<ProductItem> Products { get; set; }
    }
}
