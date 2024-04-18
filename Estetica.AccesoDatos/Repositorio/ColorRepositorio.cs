using Estetica.AccesoDatos.Data;
using Estetica.AccesoDatos.Repositorio.IRepositorio;
using Estetica.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Estetica.AccesoDatos.Repositorio
{
    public class ColorRepositorio:Repositorio<Color>,IColorRepositorio
    {
        private readonly ApplicationDbContext _db;
        public ColorRepositorio(ApplicationDbContext db):base(db)
        {
            _db= db;
        }

        public void Actualizar(Color color)
        {
            var colorBD=_db.Colores.FirstOrDefault(b=>b.Id==color.Id);
            if(colorBD!=null)
            {
                colorBD.Nombre=color.Nombre;
                colorBD.Descripcion=color.Descripcion;
                colorBD.Estado=color.Estado;
                _db.SaveChanges();
            }
        }
    }
}
