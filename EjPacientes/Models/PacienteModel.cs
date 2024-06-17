using Microsoft.AspNetCore.Mvc.Rendering;

namespace EjPacientes.Models
{
    public class PacienteModel
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public int Edad { get; set; }

        public string Sexo { get; set; }

        public string Celular { get; set; }

        public Guid? RecetaId { get; set; }

        public RecetaModel RecetaModel{ get; set; }

        public string RecetaNombre { get; set; }

        public List<SelectListItem> ListRecetas { get; set; }
    }
}