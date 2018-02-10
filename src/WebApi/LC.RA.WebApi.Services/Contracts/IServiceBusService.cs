using System.Threading.Tasks;

namespace LC.RA.WebApi.Services.Contracts
{
    public interface IServiceBusService
    {
        Task SendMessage();
    }
}