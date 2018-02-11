using System.Threading.Tasks;

namespace LC.ServiceBusAdapter.Abstractions
{
    public interface IQueueMessageHandler
    {
        Task Execute(byte[] messageBody);
    }
}