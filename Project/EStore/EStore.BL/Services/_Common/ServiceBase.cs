using System.Linq;
using System.Security.Claims;
using System.Web;
using AutoParse;
using EStore.DL.Mapping;
using Microsoft.AspNet.Identity;

namespace EStore.BL.Services._Common
{
    public abstract class ServiceBase
    {
        protected EStoreEntities Db;

        protected ServiceBase(EStoreEntities entities)
        {
            Db = entities;
        }

        protected string UserFullName
        {
            get { return HttpContext.Current.User.Identity.GetUserName(); }
        }

//        protected RoleEnum UserRole
//        {
//            get { return HttpContext.Current.User.Identity.GetUserRole(); }
//        }

        protected long UserId
        {
            get { return HttpContext.Current.User.Identity.GetUserId().TryParse<long>(); }
        }

        protected string UserEmail
        {
            get
            {
                var identity = (ClaimsIdentity) HttpContext.Current.User.Identity;
                var email = identity.Claims.Single(x => x.Type == ClaimTypes.Email).Value;
                return email;
            }
        }

//            Db.SaveChanges();
//            Db.Delete<T>(x => x.PKID == id);
//        {
//        public void DeleteById<T>(long id) where T : class, IdEntity
//
//        }
    }
}