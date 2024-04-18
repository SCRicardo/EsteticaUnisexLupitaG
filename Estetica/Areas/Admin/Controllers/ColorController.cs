using Estetica.AccesoDatos.Repositorio.IRepositorio;
using Estetica.Modelos;
using Estetica.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace Estetica.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public ColorController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Color color = new Color();
            if (id == null)
            {
                //Crear nueva Color
                color.Estado = true;
                return View(color);
            }
            color = await _unidadTrabajo.Color.obtener(id.GetValueOrDefault()); //Nos aseguramos que la información llegue correctamente
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Evita que se pueda clonar 
        public async Task<IActionResult> Upsert(Color color)
        {
            if (ModelState.IsValid)
            {
                if (color.Id == 0)
                {
                    await _unidadTrabajo.Color.Agregar(color);
                    TempData[DS.Exitosa] = "Color creada exitósamente";
                }
                else
                {
                    _unidadTrabajo.Color.Actualizar(color);
                    TempData[DS.Exitosa] = "Color actualizada exitósamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al guardar el color";
            return View(color);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var colorDB = await _unidadTrabajo.Color.obtener(id);
            if (colorDB == null)
            {
                return Json(new { success = false, message = "Error al borrar color." });
            }
            _unidadTrabajo.Color.Remover(colorDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Color borrada exitósamente." });
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> obtenerTodos()
        {
            var todos = await _unidadTrabajo.Color.obtenerTodos();
            return Json(new { data = todos });  //data es el nombre que tiene que tener la tabla por defecto para crear el JSON
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Color.obtenerTodos();
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
