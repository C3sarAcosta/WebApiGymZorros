using System.ComponentModel.DataAnnotations;

namespace WebApiGymZorros.DTOs.Usuarios
{
    public class UsuarioCrearDTO
    {
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public int NumeroExterior { get; set; }
        public int NumeroInterior { get; set; }
        public string Telefono { get; set; }
    }
}
