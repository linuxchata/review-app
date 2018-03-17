using AutoMapper;
using LC.RA.Web.Core.Domain;
using LC.RA.Web.Core.TransferObjects;
using LC.RA.Web.DataAccess.Contracts;

namespace LC.RA.Web.DataAccess.Converters
{
    public sealed class LocationConverter : ILocationConverter
    {
        private readonly IMapper mapper;

        public LocationConverter()
        {
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<Location, LocationDto>();
                c.CreateMap<LocationDto, Location>();
            });

            this.mapper = new Mapper(config);
        }

        public Location Convert(LocationDto dto)
        {
            var entity = this.mapper.Map<Location>(dto);
            return entity;
        }

        public LocationDto Convert(Location entity)
        {
            var dto = this.mapper.Map<LocationDto>(entity);
            return dto;
        }
    }
}