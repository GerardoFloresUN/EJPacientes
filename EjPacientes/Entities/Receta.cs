namespace EjPacientes.Entities
{
    public class Receta
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string Medicamento { get; set; }

        public string Dosis { get; set; }

        public string Frecuencia { get; set; }

        public List<Paciente> Pacientes { get; set; }
    }
}