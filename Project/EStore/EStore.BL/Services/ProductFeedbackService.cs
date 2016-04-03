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
    }
}
