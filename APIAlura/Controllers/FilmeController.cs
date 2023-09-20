
using Microsoft.AspNetCore.Mvc;
using APIAlura.Data;
using APIAlura.Models;
using APIAlura.Data.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace APIAlura.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;
        private ILogger<FilmeController> _logger;
        private IMapper _mapper;

        public FilmeController(FilmeContext context, ILogger<FilmeController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        public IEnumerable<ReadFilmeDTO> RecuperaFilmes([FromQuery] int skip = 0,[FromQuery] int take = 50)
        {
            return _mapper.Map<List<ReadFilmeDTO>>( _context.Filmes.Skip(skip).Take(take));
        }


        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id)
        {
            var filme = _context.Filmes
                .FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();
            var filmeDTO = _mapper.Map<ReadFilmeDTO>(filme);

            return Ok(filmeDTO);
        }

        /// <summary>
        /// Adiciona um filme ao banco de dados
        /// </summary>
        /// <param name="filmeDTO">Objeto com os campos necessários para adição</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDTO filmeDTO)
        {

            Filme filme = _mapper.Map<Filme>(filmeDTO);

            _context.Filmes.Add(filme);
            _context.SaveChanges();
            var createdUrl = Url.Action(nameof(RecuperaFilmePorId), new { id = filme.Id });
            _logger.LogInformation($"Created URL: {createdUrl}");
            return CreatedAtAction(nameof(RecuperaFilmePorId),
                new { id = filme.Id },
                filme);

        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDTO filmeDTO)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null) return NotFound();
            _mapper.Map(filmeDTO, filme);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult AtualizaFilmeParcial(int id, [FromBody] JsonPatchDocument<UpdateFilmeDTO> patch)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null) return NotFound();

            var filmeParaAtualizar = _mapper.Map<UpdateFilmeDTO>(filme);

            patch.ApplyTo(filmeParaAtualizar, ModelState);

            if (!TryValidateModel(filmeParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(filmeParaAtualizar, filme);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null) return NotFound();

            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();

        }
    }
}
