using LC.RA.Web.Core.Domain;
using LC.RA.Web.Core.TransferObjects;

namespace LC.RA.Web.DataAccess.Contracts
{
    public interface IDoctorConverter
    {
        Doctor Convert(DoctorDto dto);

        DoctorDto Convert(Doctor entity);
    }
}