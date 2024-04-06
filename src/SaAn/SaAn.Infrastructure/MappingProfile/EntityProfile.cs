using AutoMapper;
using SaAn.Domain.Entities;
using SaAn.Infrastructure.Dtos;

namespace SaAn.Infrastructure.MappingProfile;

public class EntityProfile : Profile
{
    public EntityProfile()
    {
        CreateMap<Vehicle, VehicleDto>().ReverseMap();
        CreateMap<SparePart, SparePartDto>().ReverseMap();
    }
}