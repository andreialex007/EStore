using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace EStore.Web.Code
{
    public static class AuthentificationExtensions
    {
        public const string StringValueType = "http://www.w3.org/2001/XMLSchema#string";

//        public static void SignIn(this Controllers._Common.ControllerBase controllerBase, UserItem user, bool isPersistent = false)
//        {
//            var authenticationManager = controllerBase.HttpContext.GetOwinContext().Authentication;
//            authenticationManager.SignIn();
//            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
//            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
//            identity.AddClaim(new Claim(ClaimTypes.Name, user.FullName, StringValueType));
//            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email, StringValueType));
//            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.PKID.ToString()));
//            identity.AddClaim(new Claim(ClaimTypes.Role, user.RoleId.ToString()));
//            authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
//        }
//
//        public static void SignOut(this ControllerBase controllerBase)
//        {
//            var authenticationManager = controllerBase.HttpContext.GetOwinContext().Authentication;
//            authenticationManager.SignOut();
//        }
    }
}