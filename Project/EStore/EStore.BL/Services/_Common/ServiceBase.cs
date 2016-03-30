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

        protected string UserFullName => HttpContext.Current.User.Identity.GetUserName();
        protected long UserId => HttpContext.Current.User.Identity.GetUserId().TryParse<long>();

        protected string UserEmail
        {
            get
            {
                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                var email = identity.Claims.Single(x => x.Type == ClaimTypes.Email).Value;
                return email;
            }
        }

        protected bool IsAdmin
        {
            get
            {
                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                return identity.Claims.Single(x => x.Type == "IsAdmin").Value == "True";
            }
        }
    }
}