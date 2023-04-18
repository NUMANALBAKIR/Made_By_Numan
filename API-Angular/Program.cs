using API_Angular;
using API_Angular.Controllers;
using API_Angular.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(connectionString);
});

builder.Services.AddScoped<ISubjectsAPIController, SubjectsAPIController>();

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


app.UseAuthorization();

app.MapControllers();

app.Run();

