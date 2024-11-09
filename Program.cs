using HR.Contexts;
using HR.Repositoreies;
using HR.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("C:\\Users\\zero\\Desktop\\HR Project\\HR\\Logs\\AppLog.txt",
                rollingInterval: RollingInterval.Day
                ) // path, rollingInterval(hour, day, nmonth, year, ...)
                .CreateLogger();
//C:\Users\zero\Desktop\HR Project\HR\Logs\


// Add services to the container.

//Mappre
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Services
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<HR.Repositoreies.ISalaryTierRepository, SalaryTierRepository>();

builder.Services.AddScoped<IValidationService, ValidationService>();



builder.Services.AddDbContext<HRContext>(
    options => options.UseSqlServer(builder.Configuration["ConnectionStrings:HR"]));

builder.Host.UseSerilog();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
