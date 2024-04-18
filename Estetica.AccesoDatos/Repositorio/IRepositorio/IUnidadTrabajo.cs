using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estetica.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo:IDisposable
    {
        IServicioRepositorio Servicio {  get; }
        IColorRepositorio Color { get; }
        Task Guardar();
    }
}
