using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MISA.Web062023.AMIS.API;
using MISA.Web062023.AMIS.Application;
using MISA.Web062023.AMIS.Domain;
using MISA.Web062023.AMIS.Infrastructure;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add services to the container.
// Json convert request and response
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var error = context.ModelState.Values.SelectMany(x => x.Errors);
            return new BadRequestObjectResult(new BaseException()
            {
                ErrorCode = 400,
                UserMessage = MISA.Web062023.AMIS.Domain.Resources.Exception.Exception.BadRequestException,
                DevMessage = MISA.Web062023.AMIS.Domain.Resources.Exception.Exception.BadRequestException,
                TraceId = "",
                MoreInfo = "",
                Errors = error
            });
        };
    })
    .AddJsonOptions(
    options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    }
);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:44351", "http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});



builder.Services.AddScoped<IUnitOfWork>(
    provider =>
    {
        var DbSetting = builder.Configuration.GetSection("DbSettings");
        var connectionString = $"Server={DbSetting["Server"]}; Database={DbSetting["Database"]}; Uid={DbSetting["UserId"]}; Pwd={DbSetting["Password"]}";
        return new UnitOfWork(connectionString);
    }
    );

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IFixedAssetCategoryRepository, FixedAssetCategoryRepository>();
builder.Services.AddScoped<IFixedAssetRepository, FixedAssetRepository>();
builder.Services.AddScoped<IRecordingRepository, RecordingRepository>();
builder.Services.AddScoped<IRecordedAssetRepository, RecordedAssetRepository>();
builder.Services.AddScoped<IResourceAssetRepository, ResourceAssetRepository>();
builder.Services.AddScoped<IResourceBudgetRepository, ResourceBudgetRepository>();

builder.Services.AddScoped<IDepartmentManager, DepartmentManager>();
builder.Services.AddScoped<IFixedAssetCategoryManager, FixedAssetCategoryManager>();
builder.Services.AddScoped<IFixedAssetManager, FixedAssetManager>();
builder.Services.AddScoped<IRecordingManager, RecordingManager>();
builder.Services.AddScoped<IAccountManager, AccountManager>();

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IFixedAssetCategoryService, FixedAssetCategoryService>();
builder.Services.AddScoped<IFixedAssetService, FixedAssetService>();
builder.Services.AddScoped<IRecordingService, RecordingService>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddScoped<IRecordingAssetService, RecordingAssetService>();
builder.Services.AddScoped<IRecordingAssetsRepository, RecordingAssetsRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = redirectContext =>
            {
                redirectContext.HttpContext.Response.StatusCode = 401;
                return Task.CompletedTask;
            },
            OnRedirectToAccessDenied = redirectContext =>
            {
                redirectContext.HttpContext.Response.StatusCode = 403;
                return Task.CompletedTask;
            }
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
};

app.UseCookiePolicy(cookiePolicyOptions);

app.UseAuthentication();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
