using System.Collections.Generic;

using ReviewApp.Web.Core.Domain;

namespace ReviewApp.Web.Services.Contracts
{
    public interface ILocationsConverter
    {
        List<Location> Convert(byte[] locationsMessage);
    }
}