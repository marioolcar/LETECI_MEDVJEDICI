using AutoMapper;
using SpotPicker.Model;
using SpotPicker.Model.Dtos;

namespace SpotPicker.Service
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<KorisnikDto, Korisnik>()
                .ForMember(x => x.PictureData, opt => opt.MapFrom(x => (byte[]?) null));
            CreateMap<Korisnik, KorisnikDto>()
                .ForMember(x => x.PictureData, opt => opt.MapFrom(x => (IFormFile?)null));
        }
    }
}
