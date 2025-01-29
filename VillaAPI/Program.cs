using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using VillaAPI.Data;
using VillaAPI.IRepository;
using VillaAPI.Mapping;
using VillaAPI.Models;
using VillaAPI.Repository;
using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);

#region Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("Cs"));
});
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IVillaRepository,VillaRepository>();
builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers(options=> 
{
    options.CacheProfiles.Add("Default30", new CacheProfile
    {
        Duration = 30
    });
});
builder.Services.AddResponseCaching();
#region Versioning
builder.Services.AddApiVersioning(options=>
{ 
options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1,0);
    options.ReportApiVersions = true;
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;

});
#endregion
#region Swagger Doc
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "My API v1",
        Version = "v1.0" ,
        Description = "Api to Vilaa",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Contact Me",
            Url = new Uri("https://example.com/terms"),
        },
        License = new OpenApiLicense
        {
            Name = "Exaple License",
            Url = new Uri("https://example.com/terms")
        }

    });
    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "My API v2",
        Version = "v2.0",
        Description = "Api to Vilaa version 2",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Contact Me",
            Url = new Uri("https://example.com/terms"),
        },
        License = new OpenApiLicense
        {
            Name = "Exaple License",
            Url = new Uri("https://example.com/terms")
        }

    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token in the text input below.\n\nExample: `Bearer abc123`"
    });
    // Add a global security requirement
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
                //Scheme = "oauth2",
                //         Name= "Bearer",
                //         In = ParameterLocation.Header
            },
            new List<string>()
        }
    });

});
#endregion
#region Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
#endregion
#region Authenyication
var key = builder.Configuration.GetValue<string>("ApiSettings:SecretKey");
builder.Services.AddAuthentication(a =>
  {
      a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  }).AddJwtBearer(x=> 
  { 
      x.RequireHttpsMetadata = false;
      x.SaveToken=true;
      x.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
          ValidateIssuer = false,
          ValidateAudience = false
      };
      
      
  });
#endregion
#endregion
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options=>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json","VillaAPI_V1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "VillaAPI_V2");

    });
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();