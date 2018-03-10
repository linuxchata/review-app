using System.Threading;
using System.Threading.Tasks;

namespace LC.ServiceBusAdapter.Abstractions
{
    public interface IQueueReceiverService
    {
        Task ListenForMessages(CancellationToken cancellationToken);
    }
}