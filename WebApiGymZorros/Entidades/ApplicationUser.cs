using Microsoft.AspNetCore.Identity;

namespace WebApiGymZorros.Entidades
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public int NumeroExterior { get; set; }
        public int NumeroInterior { get; set; }
        public string Telefono { get; set; }
    }
}
