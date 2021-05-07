using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ChazuraProgram.Models
{
    public class SponserConfigur : IEntityTypeConfiguration<SponsorData>
    {
        public void Configure(EntityTypeBuilder<SponsorData> builder)
        {
            builder.HasOne(p => p.User).WithMany(u => u.Sponsors).HasForeignKey(p => p.UserId)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
