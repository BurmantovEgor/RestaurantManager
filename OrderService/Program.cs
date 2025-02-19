using Microsoft.EntityFrameworkCore;
using OrderService.Abstractions;
using OrderService.Data.Configurations;
using OrderService.Data.Repository;
using OrderService.Helpers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")), ServiceLifetime.Scoped);

builder.Services.AddAutoMapper(typeof(OrderMapperProfile).Assembly);

builder.Services.AddScoped<IOrderService, OrderService.Core.Services.OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    dbContext.Database.Migrate();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    dbContext.SeedStatuses();
}
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
