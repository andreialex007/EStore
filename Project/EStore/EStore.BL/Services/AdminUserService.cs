using System.Linq;
using EStore.BL.Extensions;
using EStore.BL.Models;
using EStore.BL.Services._Common;
using EStore.DL.Mapping;

namespace EStore.BL.Services
{
    public class AdminUserService : ServiceBase
    {
        public AdminUserService(EStoreEntities entities) : base(entities)
        {
        }

        public void Save(AdminUserItem item)
        {
            var errors = item.GetValidationErrors();
            errors.ThrowIfHasErrors();

            var user = item.Id == 0
                ? Db.CreateAndAdd<tblUser>()
                : Db.Set<tblUser>().Single(x => x.Id == item.Id);

            user.Email = item.Email;
            user.FirstName = item.FirstName;
            user.LastName = item.LastName;
            user.Password = item.Password;
            user.UserName = item.UserName;
            user.IsAdmin = true;

            Db.SaveChanges();

            item.Id = user.Id;
        }

        public AdminUserItem Edit(long id)
        {
            var userItem = new AdminUserItem();

            if (id != 0)
            {
                userItem = Db.Set<tblUser>()
                    .Select(x => new AdminUserItem
                    {
                        Id = x.Id,
                        Email = x.Email,
                        Password = x.Password,
                        UserName = x.UserName,
                        FirstName = x.FirstName,
                        LastName = x.LastName
                    })
                    .Single(x => x.Id == id);
            }

            return userItem;
        }
    }
}