using APIBiblioteca.DTO;
using APIBiblioteca.Models;
using AutoMapper;

namespace APIBiblioteca.Utilidades
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<AutorCreacionDTO, Autor>()
                .ForMember(d => d.FechaNacimiento,
                    opt => opt.MapFrom(o => DateTime.Parse(o.FechaNacimiento))).ReverseMap();

            //CreateMap<Autor, AutorCreacionDTO>();

            CreateMap<Autor, AutorDTO>()
                .ForMember(d => d.FechaNacimiento,
                    opt => opt.MapFrom(o => o.FechaNacimiento.ToString("dd/MM/yyyy"))).ReverseMap();

            CreateMap<GeneroDTO, Genero>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>().ReverseMap();

            //CreateMap<Libro, LibroCreacionDTO>().ReverseMap();
            //CreateMap<Libro, LibroDTO>().ReverseMap();

            CreateMap<Libro, LibroDTO>()
                .ForMember(d => d.NombreAutor, o => o.MapFrom(src => src.Autor.Nombre))
                .ForMember(d => d.NombreGenero, o => o.MapFrom(src => src.Genero.Nombre))
                .ForMember(d => d.FechaLanzamiento, opt => opt.MapFrom(o => o.FechaLanzamiento.ToString("dd/MM/yyyy")));

            CreateMap<LibroCreacionDTO, Libro>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Autor, o => o.Ignore())
                .ForMember(d => d.Genero, o => o.Ignore())
                .ForMember(d => d.FechaLanzamiento,
                    opt => opt.MapFrom(o => DateTime.Parse(o.FechaLanzamiento)));

            CreateMap<Libro, LibroCreacionDTO>()
                .ForMember(d => d.FechaLanzamiento, opt => opt.MapFrom(o => o.FechaLanzamiento.ToString("dd/MM/yyyy")));

            CreateMap<Comentario, ComentarioDTO>().ReverseMap();

            CreateMap<ListaComentariosDTO, ComentarioDTO>().ReverseMap();

            CreateMap<Comentario, ListaComentariosDTO>().ReverseMap();
        }
    }
}
