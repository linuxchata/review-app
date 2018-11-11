using ReviewApp.Web.Core.Domain;
using ReviewApp.Web.Core.TransferObjects;

namespace ReviewApp.Web.DataAccess.Contracts
{
    public interface ILocationConverter
    {
        Location Convert(LocationDto dto);

        LocationDto Convert(Location entity);
    }
}