using RabbitMQ.Client;

namespace RabbitMQ.Core.MessageQueue
{
    public interface IConnectionChannelPool
    {
        IConnection GetConnection();

        IModel Rent();

        bool Return(IModel model);
    }
}
