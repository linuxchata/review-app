using AutoMapper;
using LC.RA.WebApi.Core.Domain;
using LC.RA.WebApi.Services.Contracts;

namespace LC.RA.WebApi.Services.Converters
{
    public sealed class LocationConverter : ILocationConverter
    {
        private readonly IMapper mapper;

        public LocationConverter()
        {
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<Location, TransferObjects.Location>();
            });

            this.mapper = new Mapper(config);
        }

        public Location Convert(TransferObjects.Location dto)
        {
            var entity = this.mapper.Map<Location>(dto);
            return entity;
        }
    }
}