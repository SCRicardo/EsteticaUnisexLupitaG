using Estetica.AccesoDatos.Data;
using Estetica.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estetica.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;
        public IServicioRepositorio Servicio {  get; private set; }
        public IColorRepositorio Color {  get; private set; }
        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db=db;
            Servicio = new ServicioRepositorio(db);
            Color =new ColorRepositorio(db);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}
