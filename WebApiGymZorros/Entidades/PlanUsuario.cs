namespace WebApiGymZorros.Entidades
{
    public class PlanUsuario
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int PlanId { get; set; }
        public Plan Plan { get; set; }
        public string UsuarioId { get; set; }
        public ApplicationUser Usuario { get; set; }
    }
}
