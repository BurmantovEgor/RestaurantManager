using Microsoft.EntityFrameworkCore;
using OrderService.Data.Configurations;
using PaymentService.Abstractions;
using PaymentService.Data.Repositories;
using PaymentService.Messaging;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PaymentDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")), ServiceLifetime.Scoped);

builder.Services.AddScoped<IPaymentOrderService, PaymentService.Services.PaymentOrderService>();
builder.Services.AddScoped<IPaymentOrderRepository, PaymentOrderRepository>();

builder.Services.AddHostedService<KafkaConsumer>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PaymentDbContext>();
    dbContext.Database.Migrate();
    dbContext.SeedStatuses();

}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
