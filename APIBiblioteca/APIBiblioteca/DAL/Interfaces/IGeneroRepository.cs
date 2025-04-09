using APIBiblioteca.Models;

namespace APIBiblioteca.DAL.Interfaces
{
    public interface IGeneroRepository : IGenericRepository<Genero>
    {
        public Task<IEnumerable<Genero>> ObtenerConLibros();
    }
}
