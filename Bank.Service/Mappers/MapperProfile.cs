using AutoMapper;
using Bank.Domain.Entities;
using Bank.Service.DTOs;

namespace Bank.Service.Mappers;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
		CreateMap<User,UserForCreationDto>().ReverseMap();
		CreateMap<User,UserForResultDto>().ReverseMap();
	}
}
