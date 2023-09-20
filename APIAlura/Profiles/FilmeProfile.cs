using APIAlura.Data.DTOs;
using APIAlura.Models;
using AutoMapper;

namespace APIAlura.Profiles
{
    public class FilmeProfile : Profile
    {
        public FilmeProfile()
        {
            CreateMap<CreateFilmeDTO, Filme>();
            CreateMap<UpdateFilmeDTO, Filme>();
            CreateMap<Filme, UpdateFilmeDTO>();
            CreateMap<Filme, ReadFilmeDTO>();
        }
    }
}
