using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ChazuraProgram.Models
{
    public class ChazuraContext:IdentityDbContext<User>
    {
        public ChazuraContext(DbContextOptions<ChazuraContext> options):base(options)
        {

        }
        
        public DbSet<MeshctaShas> MeshctaShas { get; set; }
        public DbSet<DafimShas> DafimShas { get; set; }
        public DbSet<ShasChazuraData> ShasChazuraData { get; set; }
        public DbSet<DatesChart> DatesChart { get; set; }
        public DbSet<LimudChart> LimudChart { get; set; }
        public DbSet<Completed> Completeds { get; set; }
        public DbSet<Shas1Sided> Shas1Sided { get; set; }
        public DbSet<ShasChazuraAumidData> ShasChazuraAumidData { get; set; }
        public DbSet<CustomLimud> Custom { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<SponsorData> Sponsors { get; set; }
        public DbSet<DefaultSponsor> DefaultSponsor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LimudChart>().HasOne(l => l.User).WithMany(u => u.LimudCharts);
            modelBuilder.Entity<CustomLimud>().HasOne(c => c.User).WithMany(u => u.CustomLimudim);
            modelBuilder.ApplyConfiguration(new PaymentConfiuration());
            modelBuilder.ApplyConfiguration(new SponserConfigur());
            modelBuilder.Entity<DefaultSponsor>().HasKey(H => H.DefaultId);
        }
        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string username = "admin1";
            string password = "Itsadmin1";
            

            if (await roleManager.FindByNameAsync(RoleNames.Admin) == null) 
            {
                await roleManager.CreateAsync(new IdentityRole(RoleNames.Admin));
            }
            if (await roleManager.FindByNameAsync(RoleNames.Manager) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(RoleNames.Manager));
            }
            if (await roleManager.FindByNameAsync(RoleNames.PlainUser) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(RoleNames.PlainUser));
            }
            if (await roleManager.FindByNameAsync(RoleNames.Sponsor) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(RoleNames.Sponsor));
            }
            if (await userManager.FindByNameAsync(username) == null) 
            {
                User user = new User { UserName = username,FirstName="Elimelach",LastName="Oberlander" };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, RoleNames.Admin);
                    await userManager.AddToRoleAsync(user, RoleNames.Manager);
                }
            }
        }

    }
}
