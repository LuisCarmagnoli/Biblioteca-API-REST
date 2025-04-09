using APIBiblioteca.DAL.Interfaces;
using APIBiblioteca.DTO;
using APIBiblioteca.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIBiblioteca.Controllers
{
    [Route("api/autor")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IGenericRepository<Autor> _repository;
        private readonly IMapper _mapper;

        public AutorController(IGenericRepository<Autor> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDTO>>> ObtenerTodos()
        {
            var autores = await _repository.ObtenerTodos();
            var autoresDTO = _mapper.Map<IEnumerable<AutorDTO>>(autores);
            return Ok(autoresDTO);
        }

        [HttpGet("{id}", Name = "GetAutor")]
        public async Task<ActionResult<AutorDTO>> Obtener(int id)
        {
            var autor = await _repository.Obtener(id);
            if(autor == null)
              return NotFound();

            var autorDTO = _mapper.Map<AutorDTO>(autor);
            return Ok(autorDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Crear(AutorCreacionDTO autorCreacionDTO)
        {
            var autor = _mapper.Map<Autor>(autorCreacionDTO);

            var resultado = await _repository.Insertar(autor);
            if (!resultado)
            {
                return NotFound();
            }
            var dto = _mapper.Map<AutorCreacionDTO>(autor);
            return new CreatedAtRouteResult("GetAutor", new { id = autor.Id }, dto);
        }
         
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, AutorCreacionDTO autorCreacionDTO)
        {
            var autorDesdeRepo = await _repository.Obtener(id);
            if (autorDesdeRepo == null)
                return NotFound();

            _mapper.Map(autorCreacionDTO, autorDesdeRepo);
            var resultado = await _repository.Actualizar(autorDesdeRepo);

            if (resultado)
                return NoContent();

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var autorDesdeRepo = await _repository.Obtener(id);
            if (autorDesdeRepo == null)
                return NotFound();

            var resultado = await _repository.Eliminar(id);

            if (resultado)
                return NoContent();

            return BadRequest();
        }
    }
}
