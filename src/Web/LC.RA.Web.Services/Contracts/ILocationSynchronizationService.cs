using System.Collections.Generic;
using LC.RA.Web.Core.Domain;

namespace LC.RA.Web.Services.Contracts
{
    public interface ILocationSynchronizationService
    {
        void Synchronize(IEnumerable<Location> sourceLocation);
    }
}