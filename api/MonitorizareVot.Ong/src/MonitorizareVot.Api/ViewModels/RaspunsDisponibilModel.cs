﻿using AutoMapper;
using MonitorizareVot.Domain.Ong.Models;
using System.Linq;

namespace MonitorizareVot.Ong.Api.ViewModels
{
    public class RaspunsDisponibilModel
    {
        public int IdOptiune { get; set; }
        public string TextOptiune { get; set; }
        public bool SeIntroduceText { get; set; }
    }

    public class RaspunsProfile : Profile
    {
        public RaspunsProfile()
        {
            CreateMap<Intrebare, IntrebareModel<RaspunsCompletatModel>>()
              .ForMember(src => src.Raspunsuri, c => c.MapFrom(dest => dest.RaspunsDisponibil));

            CreateMap<RaspunsDisponibil, RaspunsCompletatModel>()
                .ForMember(dest => dest.TextOptiune, c => c.MapFrom(src => src.IdOptiuneNavigation.TextOptiune))
                .ForMember(dest => dest.SeIntroduceText, c => c.MapFrom(src => src.IdOptiuneNavigation.SeIntroduceText))
                .ForMember(dest => dest.IdOptiune, c => c.MapFrom(src => src.IdRaspunsDisponibil))
                .ForMember(dest => dest.RaspunsCuFlag, c => c.MapFrom(src => src.RaspunsCuFlag))
                .ForMember(dest => dest.Value, c => c.MapFrom(src => src.Raspuns.First().Value));
        }
    }
}
