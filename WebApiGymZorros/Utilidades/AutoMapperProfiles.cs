using AutoMapper;
using WebApiGymZorros.DTOs.Planes;
using WebApiGymZorros.DTOs.Usuarios;
using WebApiGymZorros.Entidades;

namespace WebApiGymZorros.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ApplicationUser, UsuarioCrearDTO>().ReverseMap();
            CreateMap<PlanCrearDTO, Plan>().ReverseMap();
            CreateMap<PlanModificarDTO, Plan>().ReverseMap();
            CreateMap<PlanDTO, Plan>().ReverseMap();
        }
    }
}
