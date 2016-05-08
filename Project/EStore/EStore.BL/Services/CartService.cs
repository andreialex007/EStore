using System.Linq;
using EStore.BL.Extensions;
using EStore.BL.Models;
using EStore.BL.Services._Common;
using EStore.DL.Mapping;

namespace EStore.BL.Services
{
    public class CartService : ServiceBase
    {
        public CartService(EStoreEntities entities) : base(entities)
        {
        }

        public void Add(long productId)
        {
            var product = Db.GetProductItem(productId);

            if (UserInfo.Cart.Any(x => x.ProductId == productId))
                return;

            var cartItem = new CartItem
            {
                ProductId = productId,
                Price = product.Price,
                Count = 1
            };

            UserInfo.Cart.Add(cartItem);
        }
    }
}