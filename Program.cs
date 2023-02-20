using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using wordleboard.Models;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("WordleboardDbContextConnection") ?? throw new InvalidOperationException("Connection string 'WordleboardDbContextConnection' not found.");

builder.Services.AddDbContext<WordleBoardDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = false;
})
    .AddEntityFrameworkStores<WordleBoardDbContext>();


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IBoardRepository, BoardRepository>();
builder.Services.AddScoped<IWordleRepository, WordleRepository>();
builder.Services.AddScoped<IWordleResultsRepository, WordleResultsRepository>();
builder.Services.AddHttpClient<IWordleResultsRepository, WordleResultsRepository>();

builder.Services.AddScoped<UserManager<ApplicationUser>, UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>, SignInManager<ApplicationUser>>();

builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapRazorPages();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
