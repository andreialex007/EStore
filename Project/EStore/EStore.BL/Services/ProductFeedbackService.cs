using System.Collections.Generic;
using System.Linq;
using EStore.BL.Extensions;
using EStore.BL.Models.Product;
using EStore.BL.Services._Common;
using EStore.DL.Mapping;

namespace EStore.BL.Services
{
    public class ProductFeedbackService : ServiceBase
    {
        public ProductFeedbackService(EStoreEntities entities) : base(entities)
        {
        }

        public void Save(ProductFeedbackItem item)
        {
            var feedback = item.Id == 0
                ? Db.CreateAndAdd<tblProductFeedback>()
                : Db.Set<tblProductFeedback>().Single(x => x.Id == item.Id);

            feedback.Comment = item.Comment;
            feedback.Minuses = item.Minuses;
            feedback.Pluses = item.Pluses;
            feedback.ProductId = item.ProductId;
            feedback.Stars = item.Stars;
            feedback.UserName = item.UserName;

            Db.SaveChanges();

            item.Id = feedback.Id;
        }

        public List<ProductFeedbackItem> AddFeedbacks(List<ProductFeedbackItem> items, long productId)
        {
            var feedbacks = items
                .Select(x => new tblProductFeedback
                {
                    Id = x.Id,
                    ProductId = productId,
                    Comment = x.Comment,
                    Minuses = x.Minuses,
                    Pluses = x.Pluses,
                    Stars = x.Stars,
                    UserName = x.UserName
                })
                .ToList();

            Db.Set<tblProductFeedback>().AddRange(feedbacks);
            Db.SaveChanges();

            return feedbacks.Select(x => new ProductFeedbackItem
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UserName = x.UserName,
                Comment = x.Comment,
                Stars = x.Stars,
                Minuses = x.Minuses,
                Pluses = x.Pluses
            }).ToList();

        }
    }
}
