using ReviewApp.Web.Core.Domain;
using ReviewApp.Web.Core.TransferObjects;

namespace ReviewApp.Web.DataAccess.Contracts
{
    public interface IDoctorConverter
    {
        Doctor Convert(DoctorDto dto);

        DoctorDto Convert(Doctor entity);
    }
}