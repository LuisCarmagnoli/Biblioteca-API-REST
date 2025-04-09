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
    public class ComentarioController : ControllerBase
    {
        private readonly IGenericRepository<Comentario> _repository;
        private readonly IMapper _mapper;

        public ComentarioController(IGenericRepository<Comentario> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> CrearComentario(ComentarioDTO comentarioDTO)
        {
            var comentario = _mapper.Map<Comentario>(comentarioDTO);
            var respuesta = await _repository.Insertar(comentario);

            if (!respuesta)
                return BadRequest(respuesta);

            var dto = _mapper.Map<ComentarioDTO>(comentario);
            return Ok(dto);
        }
    }
}
