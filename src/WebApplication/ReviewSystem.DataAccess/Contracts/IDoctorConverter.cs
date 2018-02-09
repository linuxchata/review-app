using LC.RA.WebApi.Core.Domain;
using LC.RA.WebApi.Core.TransferObjects;

namespace LC.RA.WebApi.DataAccess.Contracts
{
    public interface IDoctorConverter
    {
        Doctor Convert(DoctorDto dto);

        DoctorDto Convert(Doctor entity);
    }
}