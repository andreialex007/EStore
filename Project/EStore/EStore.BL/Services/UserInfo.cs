using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using EStore.BL.Models;
using Microsoft.AspNet.Identity;

namespace EStore.BL.Services
{
    public static class UserInfo
    {
        public static string UserName => HttpContext.Current.User.Identity.GetUserName();

        public static string UserId => HttpContext.Current.User.Identity.GetUserId() ?? HttpContext.Current.Session.SessionID;

        public static string Email
        {
            get
            {
                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                var claim = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
                if (claim == null)
                    return string.Empty;
                var email = claim.Value;
                return email;
            }
        }

        public static bool IsAdmin
        {
            get
            {
                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                var claim = identity.Claims.FirstOrDefault(x => x.Type == "IsAdmin");
                if (claim == null)
                    return false;
                return claim.Value == "True";
            }
        }

        public static List<CartItem> Cart { get; set; } = new List<CartItem>();
    }
}