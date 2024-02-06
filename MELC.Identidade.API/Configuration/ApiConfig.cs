using MELC.Identidade.API.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MELC.Identidade.API.Models;

namespace MELC.Catalogo.API.Configuration
{
    public static class ApiConfig
    {
        public static void Initialize(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var user = new CustomUserIdentity
                {
                    FullName = "Admin Admin",
                    UserName = "Admin",
                    NormalizedUserName = "Admin",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                if (!context.Users.Any(u => u.UserName == user.UserName))
                {
                    var password = new PasswordHasher<IdentityUser>();
                    var hashed = password.HashPassword(user, "admin123-");
                    user.PasswordHash = hashed;

                    var userStore = new UserStore<IdentityUser>(context);
                    var result = userStore.CreateAsync(user).GetAwaiter().GetResult();

                    userStore.AddClaimsAsync(user, new List<Claim> { new Claim("Admin", "Admin") }).GetAwaiter().GetResult();
                }

                context.SaveChangesAsync().GetAwaiter().GetResult();
            }
        }
    }
}
