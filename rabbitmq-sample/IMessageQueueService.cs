namespace rabbitmq_sample
{
    public interface IMessageQueueService
    {
        void PublishMessage(string queueName, string message);
        string GetMessage(string queueName);
    }

}
