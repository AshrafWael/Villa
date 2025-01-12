using AutoMapper;
using Villa_Web.Dtos.AccountUserDtos;
using Villa_Web.Dtos.VillaDtos;
using Villa_Web.Dtos.VillaNumberDtos;
using Villa_Web.Models;

namespace Villa_Web.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Villa,AddVillaDto>().ReverseMap();
            CreateMap<Villa,UpdateVillaDto>().ReverseMap();
            CreateMap<Villa,ReadVillaDto>().ReverseMap();

            CreateMap<VillaNumber,AddVillaNumberDto>().ReverseMap();
            CreateMap<VillaNumber, UpdateVillaNumberDto>().ReverseMap();
            CreateMap<VillaNumber, ReadVillaNumberDto>().ReverseMap();


            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
            CreateMap<ApplicationUser, RegisterRequestDto>().ReverseMap();
            CreateMap<ApplicationUser, LoginRequestDto>().ReverseMap();
            CreateMap<ApplicationUser, LoginResponseDto>().ReverseMap();




        }
    }
}
