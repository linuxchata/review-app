using LC.RA.WebApi.Core.Domain;
using LC.RA.WebApi.Core.TransferObjects;

namespace LC.RA.WebApi.DataAccess.Contracts
{
    public interface ILocationConverter
    {
        Location Convert(LocationDto dto);

        LocationDto Convert(Location entity);
    }
}