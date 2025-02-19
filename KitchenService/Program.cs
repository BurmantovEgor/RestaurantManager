using KitchenService.Abstractions;
using KitchenService.Data;
using KitchenService.Data.Configurations;
using KitchenService.Messaging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<KitchenDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")), ServiceLifetime.Scoped);

builder.Services.AddScoped<IKitchenService, KitchenService.Services.KitchenService>();
builder.Services.AddScoped<IKitchenRepository, KitchenRepository>();

builder.Services.AddHostedService<CreateOrderCosumer>();
builder.Services.AddHostedService<UpdatePaymentStatusConsumer>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<KitchenDbContext>();
    dbContext.Database.Migrate();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<KitchenDbContext>();
    dbContext.SeedStatuses();
}
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
