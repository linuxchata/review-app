﻿using AutoMapper;
using ReviewSystem.Core.Domain;
using ReviewSystem.Core.TransferObjects;
using ReviewSystem.DataAccess.Contracts;

namespace ReviewSystem.DataAccess.Converters
{
    public sealed class SpecializationConverter : ISpecializationConverter
    {
        private readonly IMapper mapper;

        public SpecializationConverter()
        {
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<Location, LocationDto>();
                c.CreateMap<LocationDto, Location>();
            });

            this.mapper = new Mapper(config);
        }

        public Specialization Convert(SpecializationDto dto)
        {
            var entity = this.mapper.Map<Specialization>(dto);
            return entity;
        }

        public SpecializationDto Convert(Specialization entity)
        {
            var dto = this.mapper.Map<SpecializationDto>(entity);
            return dto;
        }
    }
}