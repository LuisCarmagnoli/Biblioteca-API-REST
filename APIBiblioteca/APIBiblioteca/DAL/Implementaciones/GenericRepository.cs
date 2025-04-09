using APIBiblioteca.DAL.DataContext;
using APIBiblioteca.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIBiblioteca.DAL.Implementaciones
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;  
        }

        public async Task<IEnumerable<T>> ObtenerTodos()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> Obtener(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<bool> Insertar(T entity)
        {
            bool resultado = false;
            _context.Set<T>().AddAsync(entity);
            resultado = await _context.SaveChangesAsync() > 0;
            return resultado;
        }
        public async Task<bool> Actualizar(T entity)
        {
            bool resultado = false;
            _context.Set<T>().Update(entity);
            resultado = await _context.SaveChangesAsync() > 0;
            return resultado;
        }
        public async Task<bool> Eliminar(int id)
        {
            var entity = await Obtener(id);
            if(entity == null)
            {
                return false;
            }
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
