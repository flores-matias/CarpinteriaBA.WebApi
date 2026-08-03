using CarpinteriaBA.Abstactions;
using CarpinteriaBA.Application;
using CarpinteriaBA.DataAccess;
using CarpinteriaBA.Entities.MicrosoftIdentity;
using CarpinteriaBA.Repository;
using CarpinteriaBA.Services;
using CarpinteriaBA.Services.AuthServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();


//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarpinteriaBA.WebApi", Version = "v1" });
//    var jwtSecurityScheme = new OpenApiSecurityScheme
//    {
//        BearerFormat = "JWT",
//        Name = "JWT Authentication",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.Http,
//        Scheme = JwtBearerDefaults.AuthenticationScheme,
//        Description = "Put your Token below",

//        Reference = new OpenApiReference
//        {
//            Id = JwtBearerDefaults.AuthenticationScheme,
//            Type = ReferenceType.SecurityScheme
//        }
//    };
//    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
//    c.AddSecurityRequirement(new OpenApiSecurityRequirement { { jwtSecurityScheme, Array.Empty<string>() } });
//});
//
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarpinteriaBA.WebApi", Version = "v1" });

    const string schemeId = "Bearer";

    c.AddSecurityDefinition(schemeId, new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Put your Token below",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT"
    });
    //error
    // AddSecurityRequirement ahora recibe un delegado que expone el "document"
    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference(schemeId, document),
            new List<String>()
        }

    });
});
//
builder.Services.AddDbContext<DbDataAccess>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        o => o.MigrationsAssembly("CarpinteriaBA.WebApi"));
    options.UseLazyLoadingProxies();
});

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt => {
    var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtConfig:Secret"]);
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = false,
        ValidateLifetime = true
    };
});

//builder.Services.AddIdentity<User, Role>(
//    options => options.SignIn.RequireConfirmedAccount = true).
//    AddDefaultTokenProviders().
//    AddEntityFrameworkStores<DbDataAccess>().
//    AddSignInManager<SignInManager<User>>().
//    AddRoleManager<RoleManager<Role>>().
//    AddUserManager<UserManager<User>>();

builder.Services.AddIdentity<User, Role>(
    options => options.SignIn.RequireConfirmedAccount = true).
    AddDefaultTokenProviders().
    AddEntityFrameworkStores<DbDataAccess>().
    AddSignInManager<SignInManager<User>>().
    AddRoleManager<RoleManager<Role>>().
    AddUserManager<UserManager<User>>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped(typeof(IStringService), typeof(StringService));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IApplication<>), typeof(Application<>));
builder.Services.AddScoped(typeof(IDbContext<>), typeof(DbContext<>));//corregido

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
