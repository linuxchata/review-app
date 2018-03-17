using LC.RA.Web.Core.Domain;
using LC.RA.Web.Core.TransferObjects;

namespace LC.RA.Web.DataAccess.Contracts
{
    public interface ILocationConverter
    {
        Location Convert(LocationDto dto);

        LocationDto Convert(Location entity);
    }
}