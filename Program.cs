using Reddit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplcationDBContext>();
builder.Services.AddCors(options => options.AddDefaultPolicy(policyBuilder => policyBuilder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader()
));
/*that allows or restricts web applications running in a browser from making requests to a server hosted on a different origin.
 * An "origin" is defined by the scheme (protocol), host (domain), and port of a URL, meaning that requests across different domains, schemes, or ports are considered cross-origin.
 https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-8.0
https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS
*/

/*code configures the CORS policy to allow requests from any origin,
which is useful for development environments or specific applications that need to be accessible from multiple or unknown origins.
However, allowing any origin in a production environment can expose your application to certain security risks, such as cross-site request forgery (CSRF) or data leakage.*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
