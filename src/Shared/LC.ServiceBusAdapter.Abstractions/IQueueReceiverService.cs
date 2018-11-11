using System.Threading;
using System.Threading.Tasks;

namespace ReviewApp.ServiceBusAdapter.Abstractions
{
    public interface IQueueReceiverService
    {
        Task ListenForMessages(CancellationToken cancellationToken);
    }
}