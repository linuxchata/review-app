using System.Collections.Generic;

using ReviewApp.Web.Core.Domain;

namespace ReviewApp.Web.Services.Contracts
{
    public interface ILocationSynchronizationService
    {
        void Synchronize(IEnumerable<Location> sourceLocation);
    }
}