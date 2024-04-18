using Estetica.AccesoDatos.Repositorio.IRepositorio;
using Estetica.Modelos;
using Estetica.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace Estetica.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServicioController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public ServicioController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Servicio servicio = new Servicio();
            if (id == null)
            {
                //Crear nueva Servicio
                servicio.Estado = true;
                return View(servicio);
            }
            servicio = await _unidadTrabajo.Servicio.obtener(id.GetValueOrDefault()); //Nos aseguramos que la información llegue correctamente
            if (servicio == null)
            {
                return NotFound();
            }
            return View(servicio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Evita que se pueda clonar 
        public async Task<IActionResult> Upsert(Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                if (servicio.Id == 0)
                {
                    await _unidadTrabajo.Servicio.Agregar(servicio);
                    TempData[DS.Exitosa] = "Servicio creada exitósamente";
                }
                else
                {
                    _unidadTrabajo.Servicio.Actualizar(servicio);
                    TempData[DS.Exitosa] = "Servicio actualizada exitósamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al guardar el Servicio";
            return View(servicio);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var servicioDB = await _unidadTrabajo.Servicio.obtener(id);
            if (servicioDB == null)
            {
                return Json(new { success = false, message = "Error al borrar Servicio." });
            }
            _unidadTrabajo.Servicio.Remover(servicioDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Servicio borrada exitósamente." });
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> obtenerTodos()
        {
            var todos = await _unidadTrabajo.Servicio.obtenerTodos();
            return Json(new { data = todos });  //data es el nombre que tiene que tener la tabla por defecto para crear el JSON
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Servicio.obtenerTodos();
            if (id == 0)
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && b.Id != id);
            }
            if (valor)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        #endregion
    }
}
