using AutoMapper;
using LC.RA.Web.Core.Domain;
using LC.RA.Web.Core.TransferObjects;
using LC.RA.Web.DataAccess.Contracts;

namespace LC.RA.Web.DataAccess.Converters
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