using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using Yadebs.Bll;
using Yadebs.Bll.Interfaces;
using Yadebs.Bll.Repository;
using Yadebs.Bll.Services;
using Yadebs.Db;

var builder = WebApplication.CreateBuilder(args);




builder.Services
    .AddControllers()
    .AddJsonOptions(opt => opt.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AccountingContext>(options =>
    options.UseNpgsql(
            builder.Configuration.GetConnectionString("AccountingContext"),
            o => o.UseNodaTime()
        )
    );
builder.Services.AddCors();

builder.Services.AddScoped<IAccountingService, AccountingService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IIncomeSurplusService, IncomeSurplusService>();

builder.Services.AddScoped(typeof(IRepository<,,,>), typeof(Repository<,,,>));


var app = builder.Build();

MapsterConfig.ConfigureMapster();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(corsPolicyBuilder =>
    {
        corsPolicyBuilder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }