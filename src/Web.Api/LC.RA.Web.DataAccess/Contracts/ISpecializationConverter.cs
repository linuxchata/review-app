using ReviewApp.Web.Core.Domain;
using ReviewApp.Web.Core.TransferObjects;

namespace ReviewApp.Web.DataAccess.Contracts
{
    public interface ISpecializationConverter
    {
        Specialization Convert(SpecializationDto dto);

        SpecializationDto Convert(Specialization entity);
    }
}