namespace WebApiGymZorros.DTOs.Usuarios
{
    public class RespuestaAutenticacion
    {
        public string Token { get; set; }
        public DateTime Expiracion { get; set; }
    }
}
