using HR.Contexts;
using HR.Middlewares;
using HR.Repositoreies;
using HR.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("C:\\Users\\zero\\Desktop\\HR Project\\HR\\Logs\\AppLog.txt",
                rollingInterval: RollingInterval.Day
                ) // path, rollingInterval(hour, day, nmonth, year, ...)
                .CreateLogger();

// Add services to the container.

//Mappre
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Repository
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<ISalaryTierRepository, SalaryTierRepository>();

//Service
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<ISalaryTierService, SalaryTierService>();
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

 


builder.Services.AddDbContext<HRContext>(
    options => options.UseSqlServer(builder.Configuration["ConnectionStrings:HR"]));

builder.Host.UseSerilog();


// Auth
builder.Services.AddAuthentication().AddJwtBearer(Options =>
{
    Options.TokenValidationParameters = new()
    {
        ValidIssuer = builder.Configuration["Authentication:issuer"],
        ValidAudience = builder.Configuration["Authentication:audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:secretkey"])),

        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
