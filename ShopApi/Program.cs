using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repositories;
using Repositories.Interfaces;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string? key = builder.Configuration.GetSection("Jwt:Key").Value;
string? issuer = builder.Configuration.GetSection("Jwt:Issuer").Value;
string? audience = builder.Configuration.GetSection("Jwt:Audience").Value;

Console.WriteLine(key);
// Add services to the container.
builder.Services.AddEntityFrameworkSqlite().AddDbContext<ShopDb>();
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromHours(24);
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}
)
   .AddJwtBearer(options =>
   {
       options.SaveToken = true;
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = issuer,
           ValidAudience = audience,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
           ClockSkew = TimeSpan.Zero
       };
       options.Events = new JwtBearerEvents
       {
           OnAuthenticationFailed = context =>
           {
               if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
               {
                   context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
               }
               return Task.CompletedTask;
           }
       };
   });
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    { Title = "Shop API", Version = "v1" });
});


// Add Repository
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductTagRepository, ProductTagRepository>();
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();
builder.Services.AddScoped<IUserTokenRepository, UserTokenRepository>();



var app = builder.Build();

app.UseCors(configurePolicy: options =>
{
    options.WithMethods("GET", "POST", "PUT", "DELETE");
    options.WithOrigins("https://localhost:5001");
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json",
           "Shop API Version 1");
        options.SupportedSubmitMethods(new[] {
                SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Patch,
                SubmitMethod.Put, SubmitMethod.Delete
        });
    });
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
