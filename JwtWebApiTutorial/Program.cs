using AutoMapper;
using JwtWebApiTutorial.Configurations;
using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Mappers;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Responses.SubmissionLeave;
using JwtWebApiTutorial.Services;
using JwtWebApiTutorial.Services.Interface;
using JwtWebApiTutorial.Sieve;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Sieve.Models;
using Sieve.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Date Time Culture
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("fr-FR");
});

//Enable access to HttpContext
builder.Services.AddHttpContextAccessor();

//Add cors
builder.Services.AddCors();

//Add new serializer
builder.Services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

// Add Dependency Injection for Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IReligionService, ReligionService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IActivityRecordService, ActivityRecordService>();
builder.Services.AddScoped<IApplicationSettingService, ApplicationSettingService>();
builder.Services.AddScoped<ISubmissionService, SubmissionService>();
builder.Services.AddScoped<ISubmissionLeaveService, SubmissionLeaveService>();

//AutoMapper configuration
builder.Services.AddScoped(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new UserProfile());
    cfg.AddProfile(new PositionProfile());
    cfg.AddProfile(new ReligionProfile());
    cfg.AddProfile(new AttendanceProfile());
    cfg.AddProfile(new ActivityRecordProfile());
    cfg.AddProfile(new ApplicationSettingProfile());
    cfg.AddProfile(new SubmissionProfile());
    cfg.AddProfile(new SubmissionLeaveProfile());
    cfg.AddProfile(new SubmissionAttributeProfile());
}).CreateMapper());

//Sieve configuration
builder.Services.AddScoped<ApplicationSieveProcessor>();
builder.Services.AddScoped<SieveProcessor>();
builder.Services.Configure<SieveOptions>(builder.Configuration.GetSection("Sieve"));
builder.Services.AddScoped<ISieveCustomFilterMethods, SieveCustomFilterMethods>();
builder.Services.AddScoped<ISieveCustomSortMethods, SieveCustomSortMethods>();

//Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme,
        },
    };

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Garuda Attendance API",
        Version = "v1",
        Description = string.Empty,
    });
    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        jwtSecurityScheme,
                        new List<string>()
                    },
                });
});

builder.Services.Configure<JWTConfiguration>(builder.Configuration.GetSection("JWT"));
builder.Services
    .AddAuthentication()
    .AddJwtBearer("Web", x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:SecretKey").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };
        x.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("Token-Expired", "true");
                }

                return Task.CompletedTask;
            },
        };
    });

builder.Services
    .AddAuthorization(options =>
    {
        options.DefaultPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .AddAuthenticationSchemes("Web")
            .Build();
    });

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

await app.RunAsync();
