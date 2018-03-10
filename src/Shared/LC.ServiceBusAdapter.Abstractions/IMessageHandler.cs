using System.Threading.Tasks;

namespace LC.ServiceBusAdapter.Abstractions
{
    public interface IMessageHandler
    {
        Task Execute(string replyTo, byte[] messageBody);
    }
}