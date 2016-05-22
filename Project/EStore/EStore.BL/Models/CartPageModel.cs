using System.Collections.Generic;

namespace EStore.BL.Models
{
    public class CartPageModel
    {
        public CartPageModel()
        {
            Products = new List<CartProductItem>();
            CheckOutModel = new CheckOutModel();
        }

        public List<CartProductItem> Products { get; set; }

        public CheckOutModel CheckOutModel { get; set; }
    }
}