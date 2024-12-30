using AutoMapper;
using VillaAPI.Dtos;
using VillaAPI.Dtos.VillaNumberDtos;
using VillaAPI.Models;

namespace VillaAPI.Mapping
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


        }
    }
}
