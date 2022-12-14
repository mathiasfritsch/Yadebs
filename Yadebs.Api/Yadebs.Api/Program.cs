using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Yadebs.Bll;
using Yadebs.Bll.Interfaces;
using Yadebs.Bll.Services;
using Yadebs.Db;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AccountingContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AccountingContext")));
builder.Services.AddCors();

builder.Services.AddScoped<IAccountingService, AccountingService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.RegisterMapsterConfiguration();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
