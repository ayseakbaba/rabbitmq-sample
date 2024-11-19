using RabbitMQ.Client;
using System.Text;

namespace rabbitmq_sample
{
    public class MessageQueueService : IMessageQueueService
    {
        private readonly RabbitMqConnector _connector;

        public MessageQueueService(RabbitMqConnector connector)
        {
            _connector = connector;
        }

        public void PublishMessage(string queueName, string message)
        {
            try
            {
                var channel = _connector.GetChannel();

                // Kuyruğu tanımla
                channel.QueueDeclare(queue: queueName, durable: false,exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                // Mesajı kuyruğa bırak
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);

                Console.WriteLine($"Mesaj kuyruğa gönderildi: {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Mesaj gönderiminde hata oluştu: {ex.Message}");
                throw;
            }
        }

        public string GetMessage(string queueName)
        {
            try
            {
                var channel = _connector.GetChannel();

                // Kuyruğu tanımla
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var result = channel.BasicGet(queue: queueName, autoAck: true);

                if (result == null)
                {
                    Console.WriteLine("Kuyrukta mesaj bulunamadı.");
                    return string.Empty;
                }

                var message = Encoding.UTF8.GetString(result.Body.ToArray());
                Console.WriteLine($"Kuyruktan mesaj alındı: {message}");
                return message;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Mesaj alma sırasında hata oluştu: {ex.Message}");
                throw;
            }
        }
    }
}
