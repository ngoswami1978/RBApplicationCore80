using Microsoft.EntityFrameworkCore;
using RBApplicationCore80.Data;
using Microsoft.AspNetCore.Identity;
using RBApplicationCore80.Utility;
using ResponseCache;
using ResponseCache.Config;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>
    (options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("readpolicy", builder => builder.RequireRole("Admin", "User"));
    options.AddPolicy("writepolicy",builder => builder.RequireRole("Admin"));
});


builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(policy => policy
        .Expire(TimeSpan.FromSeconds(10)));

    options.AddPolicy("PeoplePolicy", policy => policy
        .Expire(TimeSpan.FromMinutes(10))
        .Tag("PeoplePolicy_Tag"));

    options.AddPolicy("CachePost", MyCustomPolicy.Instance);  

});
/*new code*/
//builder.Services.AddMemoryCache();
//builder.Services.AddInMemoryResponseCache();
/**/


builder.Services.AddRazorPages();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseOutputCache();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapControllers().CacheOutput(p =>
//{
//    p.SetVaryByQuery("id");
//    p.Expire(TimeSpan.FromMinutes(10));
//});


app.MapRazorPages();

app.Run();
