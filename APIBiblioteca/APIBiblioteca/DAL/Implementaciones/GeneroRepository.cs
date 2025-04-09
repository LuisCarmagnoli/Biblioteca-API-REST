using APIBiblioteca.DAL.DataContext;
using APIBiblioteca.DAL.Interfaces;
using APIBiblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace APIBiblioteca.DAL.Implementaciones
{
    public class GeneroRepository : GenericRepository<Genero>, IGeneroRepository
    {
        private readonly ApplicationDbContext _context;
        public GeneroRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genero>> ObtenerConLibros()
        {
            return await _context.Generos
                                    .Include(g => g.Libros)
                                        .ThenInclude(l => l.Autor)
                                        .ToListAsync();
        }
    }
}
