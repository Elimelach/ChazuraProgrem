using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ChazuraProgram.Models
{
    public class PaymentConfiuration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasOne(p => p.User).WithMany(u => u.Payments).HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
