using System.Threading.Tasks;

namespace LC.ServiceBusAdapter.Abstractions
{
    public interface IQueueSenderService
    {
        Task SendMessage(byte[] messageBody);
    }
}