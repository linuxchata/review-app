using System.Collections.Generic;
using System.Threading.Tasks;

namespace LC.RA.Location.Api.Infrastructure.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<Models.Domain.Location>> GetLocations();
    }
}