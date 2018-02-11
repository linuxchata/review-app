using System.Threading.Tasks;

namespace LC.ServiceBusAdapter.Abstractions
{
    public interface IQueueMessageSenderService
    {
        Task SendMessage(byte[] messageBody);
    }
}