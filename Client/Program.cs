using API.Data;
using Client;
using Client.Areas.Identity.EmailSender;
using Client.Models.User;
using Client.Services;
using Client.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to container.
builder.Services.AddControllersWithViews();

// needed for identity
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(connectionString);
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppDbContext>();

// - above are identity stuff
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

// sequence of AddHttpClient is important !
builder.Services.AddHttpClient<IFoodService, FoodService>();
builder.Services.AddScoped<IFoodService, FoodService>();

builder.Services.AddHttpClient<ICartItemService, CartItemService>();
builder.Services.AddScoped<ICartItemService, CartItemService>();

builder.Services.AddHttpClient<IOrderHeaderService, OrderHeaderService>();
builder.Services.AddScoped<IOrderHeaderService, OrderHeaderService>();

builder.Services.AddHttpClient<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();

builder.Services.AddHttpClient<IAppUserService, AppUserService>();
builder.Services.AddScoped<IAppUserService, AppUserService>();

builder.Services.AddHttpClient<IBankAccountService, BankAccountService>();
builder.Services.AddScoped<IBankAccountService, BankAccountService>();

builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddRazorPages();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Guest}/{controller=Chart}/{action=Index}/{id?}"
);

app.Run();
