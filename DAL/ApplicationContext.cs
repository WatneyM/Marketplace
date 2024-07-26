using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using DAL.Models;

namespace DAL
{
    public class ApplicationContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<AttributeModel> Attributes { get; set; }
        public DbSet<AttributeGroupModel> AttributeGroups { get; set; }
        public DbSet<ProductAttributeModel> AttributesData { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options) { }
    }
}