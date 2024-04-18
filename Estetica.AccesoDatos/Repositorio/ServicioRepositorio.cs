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
    public class ServicioRepositorio:Repositorio<Servicio>,IServicioRepositorio
    {
        private readonly ApplicationDbContext _db;
        public ServicioRepositorio(ApplicationDbContext db):base(db)
        {
            _db= db;
        }

        public void Actualizar(Servicio servicio)
        {
            var servicioBD=_db.Servicios.FirstOrDefault(b=>b.Id==servicio.Id);
            if(servicioBD!=null)
            {
                servicioBD.Nombre=servicio.Nombre;
                servicioBD.Descripcion=servicio.Descripcion;
                servicioBD.Estado=servicio.Estado;
                _db.SaveChanges();
            }
        }
    }
}
