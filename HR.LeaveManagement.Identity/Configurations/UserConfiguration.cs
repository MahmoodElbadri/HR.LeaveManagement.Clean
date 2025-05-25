using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        builder.HasData(
            new ApplicationUser
            {
                Id = "5f9122b3-c9e1-4114-bda3-1a11a7165d0e",
                UserName = "admin@tmp.com",
                Email = "admin@tmp.com",
                NormalizedEmail = "ADMIN@TMP.COM",
                FirstName = "Mahmoud Elbadri",
                LastName = "System Admin",
                NormalizedUserName = "mahmo",
                PasswordHash = hasher.HashPassword(null, "Admin@123"),
                EmailConfirmed = true
            },
            new ApplicationUser
            {
                Id = "4062d586-96a6-449f-8a12-9214d7f9a4fe",
                UserName = "user@tmp.com",
                EmailConfirmed = true,
                Email = "user@tmp.com",
                NormalizedEmail = "USER@TMP.COM",
                FirstName = "Mahmoud Elbadri",
                LastName = "System User",
                NormalizedUserName = "USER",
                PasswordHash = hasher.HashPassword(null, "User@123")
            }
            );
    }
}
