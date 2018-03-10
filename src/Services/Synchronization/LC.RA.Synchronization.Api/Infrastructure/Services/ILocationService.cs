using System.Collections.Generic;
using System.Threading.Tasks;
using LC.RA.Synchronization.Api.Models.Domain;

namespace LC.RA.Synchronization.Api.Infrastructure.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetLocations();
    }
}