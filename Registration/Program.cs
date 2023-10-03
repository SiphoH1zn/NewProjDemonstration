using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Registration.Areas.Identity.Data;
using Registration.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("RegistrationDbContextConnection") ?? throw new 
    InvalidOperationException("Connection string 'RegistrationDbContextConnection' not found.");

builder.Services.AddDbContext<RegistrationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<RegistrationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<RegistrationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
