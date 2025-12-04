using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TransportadorasApi.Dto;
using TransportadorasApi.Interfaces.IRepository;
using TransportadorasApi.Interfaces.IService;
using TransportadorasApi.Model;
using TransportadorasApi.Repository;

namespace TransportadorasApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : Controller
    {
        private readonly IEnderecoService _enderecoService;
        private readonly IMapper _mapper;
        public EnderecoController(IEnderecoService enderecoRepository, IMapper mapper)
        {
            _enderecoService = enderecoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Endereco>))]
        public IActionResult GetEnderecos()
        {
            var enderecos = _mapper.Map<List<EnderecoDto>>(_enderecoService.GetEnderecos());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(enderecos);
        }
        [HttpGet("{enderecoId}")]
        [ProducesResponseType(200, Type = typeof(Endereco))]
        [ProducesResponseType(400)]
        public IActionResult GetRoda(int enderecoId)
        {
            if (!_enderecoService.EnderecoExists(enderecoId))
                return NotFound();

            var rota = _mapper.Map<EnderecoDto>(_enderecoService.GetEndereco(enderecoId));

            if(!ModelState.IsValid)
               return NotFound();

            return Ok(rota);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEndereco([FromBody]EnderecoDto enderecoCreate)
        {
            if(enderecoCreate == null)
                return BadRequest(ModelState);

            var endereco = _enderecoService.GetEnderecos()
                .Where(e => e.cep.Trim().ToUpper() == enderecoCreate.cep.TrimEnd().ToUpper() && e.Numero.Trim().ToUpper()==enderecoCreate.Numero.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (endereco != null)
            {
                ModelState.AddModelError("", "Endereço já existe");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var enderecoMap = _mapper.Map<Endereco>(enderecoCreate);

            if (!_enderecoService.CreateEndereco(enderecoMap))
            {
                ModelState.AddModelError("", "Alguma coisa deu errado ao salvar");
                return StatusCode(500, ModelState);
            }

            return Ok("Criado com sucesso");
        }

        [HttpPut("{enderecoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEndereco(int enderecoId, [FromBody] EnderecoDto updatedEndereco)
        {
            if ( updatedEndereco== null)
                return BadRequest(ModelState);

            if (enderecoId != updatedEndereco.Id)
                return BadRequest(ModelState);

            if (!_enderecoService.EnderecoExists(enderecoId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var enderecoMap = _mapper.Map<Endereco>(updatedEndereco);

            if (!_enderecoService.UpdateEndereco(enderecoMap))
            {
                ModelState.AddModelError("", "Algo deu errado ao atualizar o endereço");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{enderecoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteEndereco(int enderecoId)
        {
            if (!_enderecoService.EnderecoExists(enderecoId))
                return NotFound();

            var enderecoToDelete = _enderecoService.GetEndereco(enderecoId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_enderecoService.DeleteEndereco(enderecoToDelete))
            {
                ModelState.AddModelError("", "Algo deu errado ao deletar");
            }

            return NoContent();

        }



    }
}
