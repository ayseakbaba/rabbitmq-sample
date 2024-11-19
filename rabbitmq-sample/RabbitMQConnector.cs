using RabbitMQ.Client;
using System;

public class RabbitMqConnector : IDisposable
{
    private readonly ConnectionFactory _factory;
    private IConnection _connection;
    private IModel _channel;

    public RabbitMqConnector()
    {
        _factory = new ConnectionFactory
        {
            HostName = "localhost"
            // Eğer bir cloud RabbitMQ kullanıyorsanız, şu şekilde bir URI ayarı yapılabilir:
            // Uri = new Uri("amqp://username:password@hostname/virtualhost")
        };

        try
        {
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            Console.WriteLine("RabbitMQ bağlantısı kuruldu.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"RabbitMQ bağlantısı sırasında hata oluştu: {ex.Message}");
            throw;
        }
    }

    public IModel GetChannel()
    {
        if (_channel == null || !_channel.IsOpen)
        {
            _channel = _connection.CreateModel();
        }
        return _channel;
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        Console.WriteLine("RabbitMQ bağlantısı kapatıldı.");
    }
}
