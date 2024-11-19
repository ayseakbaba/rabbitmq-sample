using RabbitMQ.Client;
using rabbitmq_sample;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMessageQueueService, MessageQueueService>();
builder.Services.AddSingleton<RabbitMqConnector>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (var scope = app.Services.CreateScope())
{
    var queueService = scope.ServiceProvider.GetRequiredService<IMessageQueueService>();
    queueService.PublishMessage("mesajkuyrugu", "Uygulama ba�lat�ld� ve mesaj g�nderildi.");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


