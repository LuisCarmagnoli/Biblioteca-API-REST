using APIBiblioteca.Models;

namespace APIBiblioteca.DAL.Interfaces
{
    public interface ILibroRepository : IGenericRepository<Libro>
    {
        public Task<Libro> ObtenerPorIdConRelacion(int id);
    }
}
