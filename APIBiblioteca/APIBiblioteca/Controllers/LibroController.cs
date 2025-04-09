using APIBiblioteca.DAL.Interfaces;
using APIBiblioteca.DTO;
using APIBiblioteca.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly IGenericRepository<Libro> _repository;
        private readonly ILibroRepository _libroRepository;
        private readonly IMapper _mapper;

        public LibroController(IGenericRepository<Libro> repository, 
                               ILibroRepository libroRepository,
                               IMapper mapper)
        {
            _repository = repository;
            _libroRepository = libroRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroDTO>>> ObtenerTodos()
        {
            var libros = await _repository.ObtenerTodos();
            var librosDTO = _mapper.Map<IEnumerable<LibroDTO>>(libros);
            return Ok(librosDTO);
        }

        [HttpGet("{id}", Name = "GetLibro")]
        public async Task<ActionResult<LibroDTO>> Obtener(int id)
        {
            var libro = await _repository.Obtener(id);
            if (libro == null)
                return NotFound();

            var libroDTO = _mapper.Map<LibroDTO>(libro);
            return Ok(libroDTO);
        }

        [HttpGet("dataRelacionada/{id}")]
        public async Task<ActionResult<LibroDTO>> ObtenerDataRelacionada(int id)
        {
            var libro = await _libroRepository.ObtenerPorIdConRelacion(id);
            if(libro == null)
                return NotFound();

            var libroDTO = _mapper.Map<LibroDTO>(libro);
            return Ok(libroDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Crear(LibroCreacionDTO libroCreacionDTO)
        {
            var libro = _mapper.Map<Libro>(libroCreacionDTO);

            var resultado = await _repository.Insertar(libro);
            if (!resultado)
            {
                return NotFound();
            }
            var dto = _mapper.Map<LibroCreacionDTO>(libro);
            return new CreatedAtRouteResult("GetAutor", new { id = libro.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, LibroCreacionDTO libroCreacionDTO)
        {
            var libroDesdeRepo = await _repository.Obtener(id);
            if (libroDesdeRepo == null)
                return NotFound();

            _mapper.Map(libroCreacionDTO, libroDesdeRepo);
            var resultado = await _repository.Actualizar(libroDesdeRepo);

            if (resultado)
                return NoContent();

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var libroDesdeRepo = await _repository.Obtener(id);
            if (libroDesdeRepo == null)
                return NotFound();

            var resultado = await _repository.Eliminar(id);

            if (resultado)
                return NoContent();

            return BadRequest();
        }
    }
}
