using ReviewSystem.Core.Domain;
using ReviewSystem.Core.TransferObjects;

namespace ReviewSystem.DataAccess.Contracts
{
    public interface ISpecializationConverter
    {
        Specialization Convert(SpecializationDto dto);

        SpecializationDto Convert(Specialization entity);
    }
}