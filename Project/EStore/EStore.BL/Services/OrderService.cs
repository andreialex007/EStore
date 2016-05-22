using System.Data.Entity.Validation;
using System.Linq;
using EStore.BL.Exceptions;
using EStore.BL.Extensions;
using EStore.BL.Models;
using EStore.BL.Models.Product;
using EStore.BL.Services._Common;
using EStore.DL.Mapping;

namespace EStore.BL.Services
{
    public class OrderService : ServiceBase
    {
        private static readonly object Locker = new object();

        public OrderService(EStoreEntities entities) : base(entities)
        {
        }

        public void Add(CheckOutModel model)
        {
            var errors = model.GetValidationErrors();
            if (!model.IsDelivery.HasValue)
                errors.Add(new DbValidationError("IsDelivery", "Выберите способ получения товара"));
            errors.ThrowIfHasErrors();

            var order = Db.CreateAndAdd<tblOrder>();
            order.IsDelivery = model.IsDelivery;
            order.Address = model.Address;
            order.City = model.City;
            order.Email = model.Email;
            order.Phone = model.Phone;
            order.Name = model.Name;

            var productIds = UserInfo.Cart.Select(x => x.ProductId).ToList();

            var forSale = ProductSingleStateEnum.ForSale.CastTo<int>();
            var soldState = ProductSingleStateEnum.Sold.CastTo<int>();

            lock (Locker)
            {
                var productItems = Db.Set<tblProduct>()
                    .Where(x => productIds.Contains(x.Id))
                    .Select(x =>
                        new
                        {
                            x.Id,
                            x.Name,
                            Items = x.tblProductSingles
                                .Where(s => s.State == forSale)
                                .Where(s => s.OrderId == null)
                                .ToList()
                        });

                foreach (var cartItem in UserInfo.Cart)
                {
                    var count = cartItem.Count;
                    var prod = productItems.Single(x => x.Id == cartItem.ProductId);
                    var items = prod.Items.Take(count).ToList();
                    if (items.Count != count)
                        throw new ValidationException($@"Выбранный вами товар: {prod.Name} уже куплен");

                    order.tblProductSingles.AddRangeUnique(items);
                    items.ForEach(x => x.State = soldState);
                }

                Db.SaveChanges();

                model.Id = order.Id;
            }
        }
    }
}
