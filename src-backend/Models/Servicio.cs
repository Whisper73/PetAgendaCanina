namespace Models
{
    public class Servicio
    {
        public int? Id { get; set; }
        public string? InicioVigencia { get; set; }
        public string? FinVigencia { get; set; }
        public string? Descripcion { get; set; }
        public float? ValorServicioBase { get; set; }

        public Servicio()
        {

        }

        public Servicio(int id, string? iniciovigencia, string? finvigencia, string? descripcion, float? valorserviciobase)
        {
            Id = id;
            InicioVigencia = iniciovigencia;
            FinVigencia = finvigencia;
            Descripcion = descripcion;
            ValorServicioBase = valorserviciobase;
        }
    }
}
