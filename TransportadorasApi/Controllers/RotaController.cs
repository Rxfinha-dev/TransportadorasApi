using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TransportadorasApi.Interfaces.IRepository;
using TransportadorasApi.Model;

namespace TransportadorasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotaController : Controller
    {
        private readonly IRotaRepository _rotaRepository;
        private readonly IMapper _mapper;
        public RotaController(IRotaRepository rotaRepository, IMapper mapper)
        {
             _rotaRepository = rotaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Rota>))]
        public IActionResult GetRotas()
        {
            var rotas = _rotaRepository.GetRotas();

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rotas);
        }

        [HttpGet("{rotaId}")]
        [ProducesResponseType(200, Type = typeof(Rota))]
        [ProducesResponseType(400)]
        public IActionResult GetRota(int rotaId)
        {
            if (!_rotaRepository.RotaExists(rotaId))
                return NotFound();

            var rota = _rotaRepository.GetRota(rotaId);

            if(!ModelState.IsValid)
                return BadRequest();

            return Ok(rota);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRota([FromBody] Rota rotaCreate)
        {
            if (rotaCreate == null)
                return BadRequest(ModelState);

            var rota = _rotaRepository.GetRotas()
                .Where(r => r.Nome.Trim().ToUpper() == rotaCreate.Nome.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(rota != null)
            {
                ModelState.AddModelError("", "Rota já existe");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rotaMap = _mapper.Map<Rota>(rotaCreate);

            if(!_rotaRepository.CreateRota(rotaMap))
            {
                ModelState.AddModelError("", "Alguma coisa deu errado ao salvar");
                return StatusCode(500, ModelState);
            }

                return Ok("Criado com sucesso");

        }

        [HttpPut("{rotaId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRota(int rotaId, [FromBody]Rota updatedRota)
        {
            if (updatedRota == null)
                return BadRequest(ModelState);

            if (rotaId != updatedRota.Id)
                return BadRequest(ModelState);

            if (!_rotaRepository.RotaExists(rotaId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var rotaMap = _mapper.Map<Rota>(updatedRota);

            if(!_rotaRepository.UpdateRota(rotaMap))
            {
                ModelState.AddModelError("", "Algo deu errado ao atualizar a rota");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        

        [HttpDelete("{rotaId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRota(int rotaId)
        {
            if (!_rotaRepository.RotaExists(rotaId))
                return NotFound();

            var rotaToDelete = _rotaRepository.GetRota(rotaId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_rotaRepository.DeleteRota(rotaToDelete))
            {
                ModelState.AddModelError("", "Algo deu errado ao deletar");
            }

            return NoContent();

        }
    }
}
