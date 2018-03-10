using System.Threading.Tasks;

namespace LC.ServiceBusAdapter.Abstractions
{
    public interface ITopicSenderService
    {
        Task SendMessageAsync(byte[] messageBody, string sendTo, string replyTo = null);
    }
}