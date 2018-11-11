using System.Threading;
using System.Threading.Tasks;

namespace ReviewApp.ServiceBusAdapter.Abstractions
{
    public interface ITopicReceiverService
    {
        Task ReceiveMessagesAsync(string receiver, CancellationToken cancellationToken);
    }
}