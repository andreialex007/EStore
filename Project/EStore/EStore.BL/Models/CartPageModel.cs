using System.Collections.Generic;

namespace EStore.BL.Models
{
    public class CartPageModel
    {
        public CartPageModel()
        {
            Products = new List<CartProductItem>();
        }

        public List<CartProductItem> Products { get; set; }
    }
}