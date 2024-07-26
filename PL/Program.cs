using Microsoft.EntityFrameworkCore;

using DSL.Services.Declarations;
using DSL.Services.Implementations;

using DAL;
using DAL.Models;
using BLL.Managers.Declarations;
using BLL.Managers.Implementations;

WebApplicationBuilder builder;
WebApplication app;

{
    builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<ApplicationContext>(options =>
    {
        options.UseSqlServer(builder.Configuration["ConnectionStrings:Database"]);
    });

    builder.Services.AddTransient<ICategoryService, CategoryService>();
    builder.Services.AddTransient<IAttributeGroupService, AttributeGroupService>();
    builder.Services.AddTransient<IAttributeService, AttributeService>();
    builder.Services.AddTransient<IProductService, ProductService>();

    builder.Services.AddTransient<ICategoryManager, CategoryManager>();
    builder.Services.AddTransient<IAttributeGroupManager, AGModelMaintenanceManager>();
    builder.Services.AddTransient<IAttributeManager, AttributeManager>();
    builder.Services.AddTransient<IProductManager, ProductManager>();

    builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
    {
        options.Conventions
        .AddPageRoute("/Client/Catalog", "catalog");

        options.Conventions
        .AddPageRoute("/Maintenance/Maintenance", "maintenance");

        options.Conventions
        .AddPageRoute("/Maintenance/Category/Editor", "maintenance/category");
        options.Conventions
        .AddPageRoute("/Maintenance/Category/List", "maintenance/categories");

        options.Conventions
        .AddPageRoute("/Maintenance/Group/Editor", "maintenance/group");
        options.Conventions
        .AddPageRoute("/Maintenance/Group/List", "maintenance/groups");

        options.Conventions
        .AddPageRoute("/Maintenance/Attribute/Editor", "maintenance/attribute");
        options.Conventions
        .AddPageRoute("/Maintenance/Attribute/List", "maintenance/attributes");

        options.Conventions
        .AddPageRoute("/Maintenance/Product/Editor", "maintenance/product");
        options.Conventions
        .AddPageRoute("/Maintenance/Product/List", "maintenance/products");
    });
}

{
    app = builder.Build();

    app.UseDefaultFiles();
    app.UseStaticFiles();

    app.MapRazorPages();
}

app.Run();