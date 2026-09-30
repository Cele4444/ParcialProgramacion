using System.Collections.Generic;
using System.Linq;
using AccesoDatos.Datos;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Repositorios
{
    public class GenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext contexto;

        public GenericRepository(ApplicationDbContext contexto)
        {
            this.contexto = contexto;
        }

        public void Agregar(T entidad)
        {
            contexto.Set<T>().Add(entidad);
            contexto.SaveChanges();
        }

        public List<T> ObtenerTodos()
        {
            return contexto.Set<T>().ToList();
        }

        public T? ObtenerPorId(int id)
        {
            return contexto.Set<T>().Find(id);
        }

        public List<T> ObtenerTodosCon(string propiedadRelacionada)
        {
            return contexto.Set<T>()
                .Include(propiedadRelacionada)
                .ToList();
        }
    }
}