using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Persistence.Configurations;

public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        builder.HasData(
            new LeaveType()
            {
                ID = 1,
                Name = "Vacation",
                DateCreated = new DateTime(2023, 01, 01),
                DateModified = new DateTime(2023, 01, 01),
                DefaultDays = 10,
            }
        );
    }
}