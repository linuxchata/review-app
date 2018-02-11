using LC.RA.WebApi.Core.Domain;

namespace LC.RA.WebApi.Services.Contracts
{
    public interface ILocationConverter
    {
        Location Convert(TransferObjects.Location dto);
    }
}