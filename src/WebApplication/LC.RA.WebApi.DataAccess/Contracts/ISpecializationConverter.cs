using LC.RA.WebApi.Core.Domain;
using LC.RA.WebApi.Core.TransferObjects;

namespace LC.RA.WebApi.DataAccess.Contracts
{
    public interface ISpecializationConverter
    {
        Specialization Convert(SpecializationDto dto);

        SpecializationDto Convert(Specialization entity);
    }
}