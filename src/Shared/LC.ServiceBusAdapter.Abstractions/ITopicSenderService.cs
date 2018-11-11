using System.Threading.Tasks;

namespace ReviewApp.ServiceBusAdapter.Abstractions
{
    public interface ITopicSenderService
    {
        Task SendMessageAsync(byte[] messageBody, string sendTo, string replyTo = null);
    }
}