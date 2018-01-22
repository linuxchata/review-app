using ReviewSystem.Core.Domain;
using ReviewSystem.Core.TransferObjects;

namespace ReviewSystem.DataAccess.Contracts
{
    public interface IDoctorConverter
    {
        Doctor Convert(DoctorDto dto);

        DoctorDto Convert(Doctor entity);
    }
}