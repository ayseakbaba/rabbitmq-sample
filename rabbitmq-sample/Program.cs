using RabbitMQ.Client;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


ConnectionFactory factory = new ConnectionFactory();
//factory.Uri = new Uri("amqp://hkhjerrt:hcqiavAqll6-co4abXnSqUBh_hHifz-Z@hornet.rmq.cloudamqp.com/hkhjerrt");
factory.HostName = "localhost";

using (IConnection connection = factory.CreateConnection())
using (IModel channel = connection.CreateModel())
{
    channel.QueueDeclare("mesajkuyrugu", false, false, true);
    byte[] bytemessage = Encoding.UTF8.GetBytes("Kuyruða mesaj gönderme denemesi");
    channel.BasicPublish(exchange: "", routingKey: "mesajkuyrugu", body: bytemessage);
}
