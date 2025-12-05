using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TransportadorasApi.Dto;
using TransportadorasApi.Interfaces.IRepository;
using TransportadorasApi.Interfaces.IService;
using TransportadorasApi.Model;

namespace TransportadorasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotaController : Controller
    {
        private readonly IRotaService _rotaService;
        private readonly IMapper _mapper;
        public RotaController(IRotaService rotaService, IMapper mapper)
        {
             _rotaService = rotaService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Rota>))]
        public IActionResult GetRotas()
        {
            var rotas = _mapper.Map<List<RotaDto>>(_rotaService.GetRotas());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rotas);
        }

        [HttpGet("{rotaId}")]
        [ProducesResponseType(200, Type = typeof(Rota))]
        [ProducesResponseType(400)]
        public IActionResult GetRota(int rotaId)
        {
            if (!_rotaService.RotaExists(rotaId))
                return NotFound();

            var rota = _mapper.Map<RotaDto>(_rotaService.GetRota(rotaId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rota);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRota([FromBody] RotaDto rotaCreate)
        {
            if (rotaCreate == null)
                return BadRequest(ModelState);

            var rota = _rotaService.GetRotas()
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

            if(!_rotaService.CreateRota(rotaMap))
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
        public IActionResult UpdateRota(int rotaId, [FromBody]RotaDto updatedRota)
        {
            if (updatedRota == null)
                return BadRequest(ModelState);

            if (rotaId != updatedRota.Id)
                return BadRequest(ModelState);

            if (!_rotaService.RotaExists(rotaId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var rotaMap = _mapper.Map<Rota>(updatedRota);

            if(!_rotaService.UpdateRota(rotaMap))
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
            if (!_rotaService.RotaExists(rotaId))
                return NotFound();

            var rotaToDelete = _rotaService.GetRota(rotaId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_rotaService.DeleteRota(rotaToDelete))
            {
                ModelState.AddModelError("", "Algo deu errado ao deletar");
            }

            return NoContent();

        }
    }
}
