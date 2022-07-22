using System.ComponentModel.DataAnnotations;

namespace WebApiGymZorros.DTOs.Roles
{
    public class RolUsuariosDTO
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
