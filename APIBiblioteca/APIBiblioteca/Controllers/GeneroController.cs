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
    public class GeneroController : ControllerBase
    {
        private readonly IGenericRepository<Genero> _repository;
        private readonly IMapper _mapper;
        private readonly IGeneroRepository _generoRepository;

        public GeneroController(IGenericRepository<Genero> repository, 
                                IMapper mapper, 
                                IGeneroRepository generoRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _generoRepository = generoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeneroDTO>>> ObtenerTodos()
        {
            var generos = await _repository.ObtenerTodos();
            var generosDTO = _mapper.Map<IEnumerable<GeneroDTO>>(generos);
            return Ok(generosDTO);
        }

        [HttpGet("{id}", Name = "GetGenero")]
        public async Task<ActionResult<GeneroDTO>> Obtener(int id)
        {
            var genero = await _repository.Obtener(id);
            if (genero == null)
                return NotFound();

            var generoDTO = _mapper.Map<GeneroDTO>(genero);
            return Ok(generoDTO);
        }

        [HttpGet("conLibros")]
        public async Task<ActionResult<IEnumerable<GeneroDTO>>> ObtenerGeneroConLibros()
        {
            var generosConLibros = await _generoRepository.ObtenerConLibros();
            var generosConLibrosDTO = _mapper.Map<IEnumerable<GeneroDTO>>(generosConLibros);
            return Ok(generosConLibrosDTO);
        }


        [HttpPost]
        public async Task<ActionResult<GeneroDTO>> Crear([FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            var genero = _mapper.Map<Genero>(generoCreacionDTO);
            await _repository.Insertar(genero);
            var generoDTO = _mapper.Map<GeneroDTO>(genero);
            return Ok(generoDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, GeneroCreacionDTO generoCreacionDTO)
        {
            var generoDesdeRepo = await _repository.Obtener(id);
            if (generoDesdeRepo == null)
                return NotFound();

            _mapper.Map(generoCreacionDTO, generoDesdeRepo);
            var resultado = await _repository.Actualizar(generoDesdeRepo);

            if (resultado)
                return NoContent();

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var generoDesdeRepo = await _repository.Obtener(id);
            if (generoDesdeRepo == null)
                return NotFound();

            var resultado = await _repository.Eliminar(id);

            if (resultado)
                return NoContent();

            return BadRequest();
        }
    }
}
