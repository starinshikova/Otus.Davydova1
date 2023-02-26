using Microsoft.EntityFrameworkCore;
using WebApplication5.Abstractions;
using WebApplication5.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(x =>
    {
        x.UseNpgsql(builder.Configuration.GetConnectionString("db"));
        x.UseSnakeCaseNamingConvention();
    }
);

builder.Services.AddScoped(typeof(DbContext), typeof(DataContext));
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

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
