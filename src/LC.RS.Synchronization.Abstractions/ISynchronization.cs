using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace LC.RS.Synchronization.Abstractions
{
    public interface ISynchronization : IService
    {
        Task<int> Synchronize();
    }
}
