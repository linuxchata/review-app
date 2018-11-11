using AutoMapper;

using ReviewApp.Web.Core.Domain;
using ReviewApp.Web.Core.TransferObjects;
using ReviewApp.Web.DataAccess.Contracts;

namespace ReviewApp.Web.DataAccess.Converters
{
    public sealed class DoctorConverter : IDoctorConverter
    {
        private readonly IMapper mapper;

        public DoctorConverter()
        {
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<Location, LocationDto>();
                c.CreateMap<LocationDto, Location>();
            });

            this.mapper = new Mapper(config);
        }

        public Doctor Convert(DoctorDto dto)
        {
            var entity = this.mapper.Map<Doctor>(dto);
            return entity;
        }

        public DoctorDto Convert(Doctor entity)
        {
            var dto = this.mapper.Map<DoctorDto>(entity);
            return dto;
        }
    }
}