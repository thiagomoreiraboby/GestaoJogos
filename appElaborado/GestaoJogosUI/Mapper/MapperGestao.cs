using AutoMapper;
using Dominio.Model;
using GestaoJogosUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoJogosUI.Mapper
{
    public class MapperGestao : Profile
    {
        public MapperGestao()
        {
            CreateMap<Amigo, AmigoViewModel>()
                .ForMember(dest => dest.StyleOfWritting, config => config.Ignore())
                .ReverseMap();
            CreateMap<Jogo, JogoViewModel>()
                    .ForMember(dest => dest.StyleOfWritting, config => config.Ignore())
                    .ReverseMap();
            CreateMap<Usuario, UsuarioViewModel>()
                    .ForMember(dest => dest.StyleOfWritting, config => config.Ignore())
                    .ReverseMap();
        }
    }
}
