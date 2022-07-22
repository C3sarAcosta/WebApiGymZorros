using AutoMapper;
using WebApiGymZorros.DTOs.Usuarios;
using WebApiGymZorros.Entidades;

namespace WebApiGymZorros.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ApplicationUser, UsuarioCrearDTO>().ReverseMap();
        }
    }
}
