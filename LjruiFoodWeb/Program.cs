using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LjruiFoodWeb.Data;
using LjruiFoodWeb.Models;
using LjruiFoodWeb.Repositories;

using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".LjruiFood.Session";
    options.IdleTimeout = TimeSpan.FromHours(2);
});

// Add DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Repositories
builder.Services.AddScoped<ISanPhamRepository, SanPhamRepository>();
builder.Services.AddScoped<IDanhMucRepository, DanhMucRepository>();
builder.Services.AddScoped<LjruiFoodWeb.Services.IEmailService, LjruiFoodWeb.Services.EmailService>();

// Add Identity
builder.Services.AddIdentity<NguoiDung, IdentityRole<int>>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = "329767228308-55dv5cod17creqc0orl5kdpe3v3cmi0j" + ".apps.googleusercontent.com";
        options.ClientSecret = "GOCSPX-" + "esqNFI9SgEiKISHDYjauSBdkvQPj";
    })
    .AddFacebook(options =>
    {
        options.AppId = "582039182379124";
        options.AppSecret = "e7492cfa7298642bb398f82a9382ac10";
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/DangNhap";
    options.AccessDeniedPath = "/Account/TruyCapBiTuChoi";
    options.ExpireTimeSpan = TimeSpan.FromHours(12);
});

var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<NguoiDung>>();
    await DataSeeder.SeedAsync(context);
    await DataSeeder.SeedRolesAsync(roleManager);
    await DataSeeder.SeedAdminUserAsync(userManager);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
