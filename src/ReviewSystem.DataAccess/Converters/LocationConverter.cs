using AutoMapper;
using ReviewSystem.Core.Domain;
using ReviewSystem.Core.TransferObjects;
using ReviewSystem.DataAccess.Contracts;

namespace ReviewSystem.DataAccess.Converters
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