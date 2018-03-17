using LC.RA.Web.Core.Domain;
using LC.RA.Web.Core.TransferObjects;

namespace LC.RA.Web.DataAccess.Contracts
{
    public interface ISpecializationConverter
    {
        Specialization Convert(SpecializationDto dto);

        SpecializationDto Convert(Specialization entity);
    }
}