using ReviewSystem.Core.Domain;
using ReviewSystem.Core.TransferObjects;

namespace ReviewSystem.DataAccess.Contracts
{
    public interface ILocationConverter
    {
        Location Convert(LocationDto dto);

        LocationDto Convert(Location entity);
    }
}