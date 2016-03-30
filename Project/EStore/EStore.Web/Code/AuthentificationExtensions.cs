using System.Security.Claims;
using System.Web;
using EStore.BL.Models;
using EStore.Web.Controllers._Common;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace EStore.Web.Code
{
    public static class AuthentificationExtensions
    {
        public const string StringValueType = "http://www.w3.org/2001/XMLSchema#string";

        public static void SignIn(this ControllerBase controllerBase, UserItem user, bool isAdmin = false)
        {
            var authenticationManager = controllerBase.HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignIn();
            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.FullName, StringValueType));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email, StringValueType));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            identity.AddClaim(new Claim("IsAdmin", isAdmin.ToString()));
            authenticationManager.SignIn(new AuthenticationProperties {IsPersistent = true}, identity);
        }

        public static void SignOut(this ControllerBase controllerBase)
        {
            var authenticationManager = controllerBase.HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
        }
    }
}