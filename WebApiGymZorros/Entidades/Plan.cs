namespace WebApiGymZorros.Entidades
{
    public class Plan
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
        //public List<PlanUsuario> PlanesUsuarios { get; set; }
    }
}
