using Microsoft.AspNetCore.Identity;

namespace MELC.Identidade.API.Models
{
    public class CustomUserIdentity : IdentityUser
    {
        public string FullName { get; set; }
    }
}
