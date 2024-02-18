using Microsoft.EntityFrameworkCore;
using Yadebs.Bll;
using Yadebs.Bll.Interfaces;
using Yadebs.Bll.Services;
using Yadebs.Db;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AccountingContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("AccountingContext")));
builder.Services.AddCors();

builder.Services.AddScoped<IAccountingService, AccountingService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

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
