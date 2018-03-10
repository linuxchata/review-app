using System.Threading;
using System.Threading.Tasks;

namespace LC.ServiceBusAdapter.Abstractions
{
    public interface ITopicReceiverService
    {
        Task ReceiveMessagesAsync(string receiver, CancellationToken cancellationToken);
    }
}