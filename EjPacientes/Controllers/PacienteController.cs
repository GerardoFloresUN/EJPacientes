using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EjPacientes.Models;
using EjPacientes.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EjPacientes.Controllers;

public class PacienteController : Controller
{
    private readonly ILogger<PacienteController> _logger;
    private readonly ApplicationDbContext _context;

    public PacienteController(ILogger<PacienteController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult PacienteList()
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

[HttpGet]
    public IActionResult PacienteEdit(Guid Id)
        {
            Paciente? PacienteActualizar = this._context.Pacientes.Where(p => p.Id == Id).FirstOrDefault();
            if (PacienteActualizar == null)
            {
                return RedirectToAction ("PacienteList");
            }
            PacienteModel  model = new PacienteModel
            {
                Id = PacienteActualizar.Id,
            Nombre = PacienteActualizar.Nombre,
            Edad = PacienteActualizar.Edad,
            Sexo = PacienteActualizar.Sexo,
            Celular = PacienteActualizar.Celular
            };
            return View(model);
        
        }

        [HttpPost]
        public IActionResult PacienteEdit(PacienteModel model)
        {
            if(ModelState.IsValid)
            {
                Paciente PacienteActualizar = this._context.Pacientes.Where(p => p.Id == model.Id).First();
                if(PacienteActualizar == null)
                {
                    return RedirectToAction("PacienteList");
                }
                PacienteActualizar.Id = model.Id;
                PacienteActualizar.Nombre = model.Nombre;
                PacienteActualizar.Edad = model.Edad;
                PacienteActualizar.Sexo = model.Sexo;
                PacienteActualizar.Celular = model.Celular;
                this._context.Pacientes.Update(PacienteActualizar);
                this._context.SaveChanges();
                return RedirectToAction("PacienteList");
            }
            return View(model);

        }

    public IActionResult PacienteAdd()
        {
            return View();
        }

    [HttpPost]
    public IActionResult PacienteAdd(PacienteModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var PacienteEntity = new Paciente();
        PacienteEntity.Id = new Guid();
        PacienteEntity.Nombre = model.Nombre;
        PacienteEntity.Edad = model.Edad;
        PacienteEntity.Sexo = model.Sexo;
        PacienteEntity.Celular = model.Celular;

        this._context.Pacientes.Add(PacienteEntity);
        this._context.SaveChanges();

        return RedirectToAction("PacienteList", "Paciente");
    }

    

     public IActionResult PacienteDeleted(Guid Id)
         {
             Paciente? PacienteBorrar = this._context.Pacientes.Where(p => p.Id == Id).FirstOrDefault();
            
             if (PacienteBorrar == null)
             {
                 return RedirectToAction("PacienteList", "Paciente");
             }

            PacienteModel model = new PacienteModel();
             model.Id = PacienteBorrar.Id;
             model.Nombre = PacienteBorrar.Nombre;
             model.Edad = PacienteBorrar.Edad;
             model.Sexo = PacienteBorrar.Sexo;
             model.Celular = PacienteBorrar.Celular;

             return View(model);
         }

         [HttpPost]
         public IActionResult PacienteDeleted(PacienteModel paciente)
         {
            bool exits = this._context.Pacientes.Any(p => p.Id == paciente.Id);
            if(!exits)
            {
                return View(paciente);
            }

            Paciente pacienteEntity = this._context.Pacientes.Where(p => p.Id == paciente.Id).First();
            
            this._context.Pacientes.Remove(pacienteEntity);
            this._context.SaveChanges();

            return RedirectToAction("PacienteList","Paciente");
         }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}