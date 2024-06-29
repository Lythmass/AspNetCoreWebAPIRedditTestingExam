using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Reddit;
using Reddit.Filters;
using Reddit.Mapper;
using Reddit.Middlewares;
using Reddit.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add<ModelValidationActionFilter>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SqliteDb")));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
               builder => builder.AllowAnyOrigin()
                                 .AllowAnyMethod()
                                 .AllowAnyHeader());
});
builder.Services.AddSingleton<IMapper, Mapper>();
builder.Services.AddScoped<IPostsRepository, PostsRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
   context.Response.Headers.Add("Content-Type", "application/json");
   await next();
});


app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapGet("/throws", (context) => throw new Exception("Exception my bad"));

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
