using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using DAL.Models;

namespace DAL
{
    public class ApplicationContext(DbContextOptions options)
        : IdentityDbContext<ApplicationUser>(options)
    {
        private readonly IdentityRole _verifiedUserRole = new IdentityRole()
        {
            Name = "Customer",
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            NormalizedName = "CUSTOMER"
        };
        private readonly IdentityRole _unverifiedUserRole = new IdentityRole()
        {
            Name = "Unverified",
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            NormalizedName = "UNVERIFIED"
        };
        private readonly IdentityRole _adminRole = new IdentityRole()
        {
            Name = "Administrator",
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            NormalizedName = "ADMINISTRATOR"
        };

        private readonly ApplicationUser _adminUser = new ApplicationUser()
        {
            Firstname = "Marketplace",
            Lastname = "Administrator",
            UserName = "Administrator",
            NormalizedUserName = "ADMINISTRATOR",
            Email = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            PasswordHash = new PasswordHasher<ApplicationUser>()
                .HashPassword(null!, "Administrator"),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            PhoneNumber = "1234567890",
            LockoutEnd = null,
            LockoutEnabled = false,
            AccessFailedCount = 0
        };

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<AttributeModel> Attributes { get; set; }
        public DbSet<AttributeGroupModel> AttributeGroups { get; set; }
        public DbSet<ProductAttributeModel> AttributesData { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(_verifiedUserRole);
            builder.Entity<IdentityRole>().HasData(_unverifiedUserRole);
            builder.Entity<IdentityRole>().HasData(_adminRole);

            builder.Entity<ApplicationUser>().HasData(_adminUser);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>()
            {
                UserId = _adminUser.Id,
                RoleId = _adminRole.Id
            });

            base.OnModelCreating(builder);
        }
    }
}