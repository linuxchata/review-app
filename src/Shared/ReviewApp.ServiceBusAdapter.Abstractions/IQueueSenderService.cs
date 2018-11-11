using System.Threading.Tasks;

namespace ReviewApp.ServiceBusAdapter.Abstractions
{
    public interface IQueueSenderService
    {
        Task SendMessage(byte[] messageBody);
    }
}