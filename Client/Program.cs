using Client;
using Client.Services;
using Client.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

// necessary
builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddHttpClient<IFoodService, FoodService>();

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
    pattern: "{area=Guest}/{controller=OrderFood}/{action=Index}/{id?}");

app.Run();
