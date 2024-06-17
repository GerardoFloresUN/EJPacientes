namespace EjPacientes.Models
{
    public class RecetaModel
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string Medicamento { get; set; }

        public string Dosis { get; set; }

        public string Frecuencia { get; set; }
    }
}