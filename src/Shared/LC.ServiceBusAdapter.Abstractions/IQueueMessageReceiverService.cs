using System.Threading;
using System.Threading.Tasks;

namespace LC.ServiceBusAdapter.Abstractions
{
    public interface IQueueMessageReceiverService
    {
        Task ListenForMessages(CancellationToken cancellationToken);
    }
}