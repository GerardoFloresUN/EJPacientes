using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EjPacientes.Models;
using EjPacientes.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EjPacientes.Controllers;

public class RecetaController : Controller
{
    private readonly ILogger<RecetaController> _logger;
    private readonly ApplicationDbContext _context;

    public RecetaController(ILogger<RecetaController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult RecetaList()
    {
        

        List<RecetaModel> list = new List<RecetaModel>();
        list = _context.Recetas.Select(r => new RecetaModel()
        {
            Id = r.Id,
            Nombre = r.Nombre,
            Medicamento = r.Medicamento,
            Dosis = r.Dosis,
            Frecuencia = r.Frecuencia,
            
        }).ToList();

        return View(list);
    }

[HttpGet]
    public IActionResult RecetaEdit(Guid Id)
        {
            Receta? RecetaActualizar = this._context.Recetas.Where(r => r.Id == Id).FirstOrDefault();
            if (RecetaActualizar == null)
            {
                return RedirectToAction ("RecetaList");
            }
            RecetaModel  model = new RecetaModel
            {
                Id = RecetaActualizar.Id,
                Nombre = RecetaActualizar.Nombre,
            Medicamento = RecetaActualizar.Medicamento,
            Dosis = RecetaActualizar.Dosis,
            Frecuencia = RecetaActualizar.Frecuencia,
            
            };
            return View(model);
        
        }

        [HttpPost]
        public IActionResult RecetaEdit(RecetaModel model)
        {
            if(ModelState.IsValid)
            {
                Receta RecetaActualizar = this._context.Recetas.Where(r => r.Id == model.Id).First();
                if(RecetaActualizar == null)
                {
                    return RedirectToAction("RecetaList");
                }
                RecetaActualizar.Id = model.Id;
                RecetaActualizar.Nombre = model.Nombre;
                RecetaActualizar.Medicamento = model.Medicamento;
                RecetaActualizar.Dosis = model.Dosis;
                RecetaActualizar.Frecuencia = model.Frecuencia;
                this._context.Recetas.Update(RecetaActualizar);
                this._context.SaveChanges();
                return RedirectToAction("RecetaList");
            }
            return View(model);

        }

    public IActionResult RecetaAdd()
        {
            return View();
        }

    [HttpPost]
    public IActionResult RecetaAdd(RecetaModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var RecetaEntity = new Receta();
        RecetaEntity.Id = new Guid();
        RecetaEntity.Nombre = model.Nombre;
        RecetaEntity.Medicamento = model.Medicamento;
        RecetaEntity.Dosis = model.Dosis;
        RecetaEntity.Frecuencia = model.Frecuencia;

        this._context.Recetas.Add(RecetaEntity);
        this._context.SaveChanges();

        return RedirectToAction("RecetaList", "Receta");
    }

    

     public IActionResult RecetaDeleted(Guid Id)
         {
             Receta? RecetaBorrar = this._context.Recetas.Where(r => r.Id == Id).FirstOrDefault();
            
             if (RecetaBorrar == null)
             {
                 return RedirectToAction("RecetaList", "Receta");
             }

            RecetaModel model = new RecetaModel();
             model.Id = RecetaBorrar.Id;
             model.Nombre = RecetaBorrar.Nombre;
             model.Medicamento = RecetaBorrar.Medicamento;
             model.Dosis = RecetaBorrar.Dosis;
             model.Frecuencia = RecetaBorrar.Frecuencia;

             return View(model);
         }

         [HttpPost]
         public IActionResult RecetaDeleted(RecetaModel receta)
         {
            bool exits = this._context.Recetas.Any(r => r.Id == receta.Id);
            if(!exits)
            {
                return View(receta);
            }

            Receta recetaEntity = this._context.Recetas.Where(r => r.Id == receta.Id).First();
            
            this._context.Recetas.Remove(recetaEntity);
            this._context.SaveChanges();

            return RedirectToAction("RecetaList","Receta");
         }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}