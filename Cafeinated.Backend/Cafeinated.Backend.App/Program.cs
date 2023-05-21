using System.Text;
using Cafeinated.Backend.App.Mapper;
using Cafeinated.Backend.Core.Database;
using Cafeinated.Backend.Core.Entities;
using Cafeinated.Backend.Infrastructure.Repositories;
using Cafeinated.Backend.Infrastructure.Repositories.Abstractions;
using Cafeinated.Backend.Infrastructure.Services;
using Cafeinated.Backend.Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureAppConfiguration((builderContext, config) =>
{
    config
        .AddJsonFile("secrets/appsettings.secrets.json", optional: true) 
        .AddEnvironmentVariables();
});
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddCors(options =>
{
    options.AddPolicy("CafeinatedCorsPolicy", builder =>
    {
        builder
            .WithOrigins("http://localhost:4200", "https://cafeinated.edicz.com")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

services.AddDefaultIdentity<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDBContext>()
    .AddDefaultTokenProviders();

services.AddAuthentication()
    .AddJwtBearer(x =>
    {
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(configuration.GetSection("JWT:Secret").Value)),
            ValidateIssuer = true,
            ValidIssuer = configuration.GetSection("JWT:Issuer").Value,
            ValidateAudience = false,
            RequireExpirationTime = false,
            ValidateLifetime = true
        }; 
    });

services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 3;
    options.SignIn.RequireConfirmedEmail = true;
});

var connectionString = configuration.GetConnectionString("Default");
services.AddDbContext<AppDBContext>(options => options.UseNpgsql(connectionString, 
    x => x.MigrationsAssembly("Cafeinated.Backend.Infrastructure")));

services.AddAutoMapper(typeof(MappingProfile));

services.AddScoped<IGenericRepository<CoffeeShop>, CoffeeShopRepository>();
services.AddScoped<IGenericRepository<CoffeeType>, CoffeeTypeRepository>();
services.AddScoped<IGenericRepository<Order>, OrderRepository>();
services.AddScoped<IUploadManager, UploadManager>();
services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDBContext>();
    Console.WriteLine("Running EF migrations...");
    dbContext.Database.Migrate();
}

var contentDir = configuration["Content_Directory"];
contentDir = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), contentDir));
if (!Directory.Exists(contentDir))
{
    Console.WriteLine("Creating CONTENT_DIRECTORY: " + contentDir);
    Directory.CreateDirectory(contentDir);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(contentDir),
    RequestPath = "/content",
    ServeUnknownFileTypes = true
});

app.UseHttpsRedirection();

app.UseCors("CafeinatedCorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();