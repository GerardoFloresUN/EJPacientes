using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EjPacientes.Models;
using EjPacientes.Entities;

namespace EjPacientes.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        // Paciente paciente= new Paciente();
        // paciente.Nombre="Gerardo Flores";
        // paciente.Edad = 21;
        // paciente.Sexo= "Masculino";
        // paciente.Celular= "8114753018";

        // this._context.Pacientes.Add(paciente);
        // this._context.SaveChanges();

        // //actualizar
        // Paciente pacienteActualiza = this._context.Pacientes.Where(c=> c.Id == new Guid("1936B992-C725-45C8-45D7-08DC867BF714")).First();
        // paciente.Nombre = "Diego";
        // this._context.Pacientes.Update(pacienteActualiza);
        // this._context.SaveChanges();

        // borrar
        // Paciente pacienteborrado = this._context.Pacientes.Where(c=> c.Id == new Guid("1936B992-C725-45C8-45D7-08DC867BF714")).First();
        // this._context.Pacientes.Remove(pacienteborrado);
        // this._context.SaveChanges();

        List<PacienteModel> list = new List<PacienteModel>();
        list = _context.Pacientes.Select(p => new PacienteModel()
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Edad = p.Edad,
            Sexo = p.Sexo,
            Celular = p.Celular
        }).ToList();

        return View(list);
    }

    public IActionResult PacienteEdit()
        {
            return View();
        }

    public IActionResult PacienteAdd()
        {
            return View();
        }

    public IActionResult PacienteDeleted()
        {
            return View();
        }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
