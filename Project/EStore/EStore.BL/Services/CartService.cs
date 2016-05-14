using System.Data.Entity;
using System.Linq;
using EStore.BL.Extensions;
using EStore.BL.Models;
using EStore.BL.Services._Common;
using EStore.BL.Utils;
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

        public void Remove(long productId)
        {
            if (UserInfo.Cart.All(x => x.ProductId != productId))
                return;

            var cartItem = UserInfo.Cart.Single(x => x.ProductId == productId);
            cartItem.Count -= 1;
            if (cartItem.Count == 0)
                UserInfo.Cart.RemoveAll(x => x.ProductId == productId);
        }

        public CartPageModel GetCart()
        {
            var model = new CartPageModel();

            var productIds = UserInfo.Cart.Select(x => x.ProductId).ToList();
            var products = Db.Set<tblProduct>().Include(x=>x.tblFiles).Where(x => productIds.Contains(x.Id)).ToList();

            foreach (var product in products)
            {
                var cartItem = UserInfo.Cart.Single(x => x.ProductId == product.Id);
                var item = new CartProductItem
                {
                    ProductId = cartItem.ProductId,
                    Price = cartItem.Price,
                    Count = cartItem.Count,
                    ProductName = product.Name,
                    Description = CommonUtils.StripHtml(product.Descripton).Trim(),
                    Image = product.tblFiles.Any() ? product.tblFiles.OrderBy(f=>f.Position).First().Path : string.Empty
                };
                model.Products.Add(item);
            }
            return model;
        }
    }
}