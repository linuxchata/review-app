using System.Threading.Tasks;

namespace ReviewApp.ServiceBusAdapter.Abstractions
{
    public interface IMessageHandler
    {
        Task Execute(string replyTo, byte[] messageBody);
    }
}