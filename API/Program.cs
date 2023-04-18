using API;
using API.Data;
using API.DatabaseInitializer;
using API.Models;
using API.Repository;
using API.Repository.IRepository;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(connectionString);
});

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API I used to develop the site.",
        Description = "Numan's Swagger-API."
        //,Contact = new OpenApiContact
        //{
        //    Url = new Uri("https://madebynumanclient.azurewebsites.net/")
        //}
    })
);

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();

var app = builder.Build();



// Configure HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseSwaggerUI(options =>
//{
//    options.RoutePrefix = string.Empty;
//    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
//});

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

SeedDatabase();

app.UseAuthorization();

app.MapControllers();

app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var databaseInitializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
        databaseInitializer.Initialize();
    }
}