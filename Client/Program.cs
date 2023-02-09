using Client;
using Client.Services;
using Client.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

// sequence is important !
builder.Services.AddHttpClient<IFoodService, FoodService>();
builder.Services.AddScoped<IFoodService, FoodService>();

builder.Services.AddHttpClient<ICartItemService, CartItemService>();
builder.Services.AddScoped<ICartItemService, CartItemService>();

builder.Services.AddHttpClient<IOrderHeaderService, OrderHeaderService>();
builder.Services.AddScoped<IOrderHeaderService, OrderHeaderService>();

builder.Services.AddHttpClient<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Guest}/{controller=Cart}/{action=Summary}/{id?}"
);

app.Run();
