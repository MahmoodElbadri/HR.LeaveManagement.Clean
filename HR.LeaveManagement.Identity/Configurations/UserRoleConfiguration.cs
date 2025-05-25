using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "ee8236a5-e39a-4671-a6de-2b828a9bed51",
                UserId = "5f9122b3-c9e1-4114-bda3-1a11a7165d0e"
            },
            new IdentityUserRole<string>
            {
                UserId = "4062d586-96a6-449f-8a12-9214d7f9a4fe",
                RoleId = "0ccf3599-43a1-48e8-a097-220e0aeb5e8f"
            }
            );
    }
}
