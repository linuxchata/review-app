using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewApp.Location.Infrastructure.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<Core.Domain.Location>> GetLocations();
    }
}