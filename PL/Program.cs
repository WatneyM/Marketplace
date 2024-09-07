using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using BLL.Managers.Declarations;
using BLL.Managers.Implementations;

using DAL;

using DSL.Services.Declarations;
using DSL.Services.Implementations;

WebApplicationBuilder builder;
WebApplication app;

builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:Database"]);
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opts =>
{
    opts.Lockout.AllowedForNewUsers = false;

    opts.Password.RequiredLength = 12;
    opts.Password.RequireUppercase = true;
    opts.Password.RequireLowercase = true;
    opts.Password.RequireDigit = false;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequiredUniqueChars = 0;
}).AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddAuthentication().AddGoogle(opts =>
{
    opts.ClientId = builder.Configuration["Authentication:Google:Id"]!;
    opts.ClientSecret = builder.Configuration["Authentication:Google:Secret"]!;
    opts.AccessDeniedPath = "/authentication";
    opts.ReturnUrlParameter = "";
});

builder.Services.ConfigureApplicationCookie(opts =>
{
    opts.AccessDeniedPath = "/authentication";
    opts.LoginPath = "/authentication";

    opts.ExpireTimeSpan = TimeSpan.FromDays(7D);
});

builder.Services.AddSession();

builder.Services.AddTransient<ISessionService, SessionService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IMailerService, MailerService>();

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IAttributeGroupService, AttributeGroupService>();
builder.Services.AddTransient<IAttributeService, AttributeService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<ISupplyService, SupplyService>();

builder.Services.AddTransient<ICategoryManager, CategoryManager>();
builder.Services.AddTransient<IAttributeGroupManager, AGModelMaintenanceManager>();
builder.Services.AddTransient<IAttributeManager, AttributeManager>();
builder.Services.AddTransient<IProductManager, ProductManager>();
builder.Services.AddTransient<IOrderManager, OrderManager>();
builder.Services.AddTransient<ISupplyManager, SupplyManager>();

builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
{
    options.Conventions
    .AddPageRoute("/Client/Cart", "cart/{key?}");
    options.Conventions
    .AddPageRoute("/Client/Orders", "orders");
    options.Conventions
    .AddPageRoute("/Client/Categories", "categories");
    options.Conventions
    .AddPageRoute("/Client/Catalog", "catalog/{key}/{filter?}");
    options.Conventions
    .AddPageRoute("/Client/Product", "product/{key?}");

    options.Conventions
    .AddPageRoute("/Authentication/Authentication", "authentication");
    options.Conventions
    .AddPageRoute("/Authentication/Registration", "registration");
    options.Conventions
    .AddPageRoute("/Authentication/Recovery", "recovery");

    options.Conventions
    .AddPageRoute("/Maintenance/Maintenance", "maintenance/{os:int=0}");
    options.Conventions
    .AddPageRoute("/Maintenance/Storage", "storage/{os:int=0}");

    options.Conventions
    .AddPageRoute("/Maintenance/Supply", "maintenance/supply/{key?}");

    options.Conventions
    .AddPageRoute("/Maintenance/Category/Editor", "maintenance/category/{key?}");
    options.Conventions
    .AddPageRoute("/Maintenance/Category/List", "maintenance/categories");

    options.Conventions
    .AddPageRoute("/Maintenance/Group/Editor", "maintenance/group/{key?}");
    options.Conventions
    .AddPageRoute("/Maintenance/Group/List", "maintenance/groups");

    options.Conventions
    .AddPageRoute("/Maintenance/Attribute/Editor", "maintenance/attribute/{key?}");
    options.Conventions
    .AddPageRoute("/Maintenance/Attribute/List", "maintenance/attributes");

    options.Conventions
    .AddPageRoute("/Maintenance/Product/Editor", "maintenance/product/{ckey?}/{pkey?}");
    options.Conventions
    .AddPageRoute("/Maintenance/Product/List", "maintenance/products/{key?}");
});

app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();