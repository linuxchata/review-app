using System.Collections.Generic;
using LC.RA.WebApi.Core.Domain;

namespace LC.RA.WebApi.Services.Contracts
{
    public interface ILocationSynchronizationService
    {
        void Synchronize(IEnumerable<Location> sourceLocation);
    }
}