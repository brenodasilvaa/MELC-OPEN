using System.Security.Claims;

namespace MELC.WebApp.MVC.Extensions
{
    public interface IUser
    {
        string Name { get; }
        Guid ObterUserId();
        string ObterUserUniqueName();
        string ObterUserToken();
        bool EstaAutenticado();
        bool PossuiRole(string role);
        IEnumerable<Claim> ObterClaims();
        HttpContext ObterHttpContext();
    }

    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public Guid ObterUserId()
        {
            return EstaAutenticado() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }

        public string ObterUserUniqueName()
        {
            return EstaAutenticado() ? _accessor.HttpContext.User.GetUserUniqueName() : "";
        }

        public string ObterUserToken()
        {
            return EstaAutenticado() ? _accessor.HttpContext.User.GetUserToken() : "";
        }

        public bool EstaAutenticado()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool PossuiRole(string role)
        {
            return _accessor.HttpContext.User.IsInRole(role);
        }

        public IEnumerable<Claim> ObterClaims()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public HttpContext ObterHttpContext()
        {
            return _accessor.HttpContext;
        }
    }

    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst("sub");
            return claim?.Value;
        }

        public static string GetUserUniqueName(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst("unique_name");
            return claim?.Value;
        }

        public static string GetUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst("name");
            return claim?.Value;
        }

        public static string GetUserNameForamtted(this ClaimsPrincipal principal)
        {
            var userName = principal.GetUserName();
            var userNameFormatted = string.Empty;

            if (string.IsNullOrEmpty(userName))
                return userName;

            var userNameSplit = userName.Split(' ');

            foreach (var name in userNameSplit)
            {
                if (userNameFormatted.Length == 2)
                    return userNameFormatted.ToUpper();

                if (!string.IsNullOrEmpty(name))
                    userNameFormatted += name.ToCharArray().First();
            }

            return userNameFormatted.ToUpper();

        }

        public static string GetUserToken(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst("JWT");
            return claim?.Value;
        }

        public static bool IsAdmin(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            return principal.Claims.Any(x => x.Type == "Admin" && x.Value == "Admin");

        }
    }
}