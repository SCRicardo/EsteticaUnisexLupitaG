using Estetica.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estetica.AccesoDatos.Repositorio.IRepositorio
{
    public interface IColorRepositorio:IRepositorio<Color>
    {
        void Actualizar(Color color);
    }
}
