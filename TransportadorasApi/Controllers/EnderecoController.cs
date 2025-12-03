using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TransportadorasApi.Interfaces.IRepository;
using TransportadorasApi.Model;
using TransportadorasApi.Repository;

namespace TransportadorasApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : Controller
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IMapper _mapper;
        public EnderecoController(IEnderecoRepository enderecoRepository, IMapper mapper)
        {
            _enderecoRepository = enderecoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Endereco>))]
        public IActionResult GetEnderecos()
        {
            var enderecos = _enderecoRepository.GetEnderecos();

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(enderecos);
        }
        [HttpGet("{enderecoId}")]
        [ProducesResponseType(200, Type = typeof(Endereco))]
        [ProducesResponseType(400)]
        public IActionResult GetRoda(int enderecoId)
        {
            if (!_enderecoRepository.EnderecoExists(enderecoId))
                return NotFound();

            var rota = _enderecoRepository.GetEndereco(enderecoId);

            if(!ModelState.IsValid)
               return NotFound();

            return Ok(rota);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEndereco([FromBody]Endereco enderecoCreate)
        {
            if(enderecoCreate == null)
                return BadRequest(ModelState);

            var endereco = _enderecoRepository.GetEnderecos()
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

            if (!_enderecoRepository.CreateEndereco(enderecoMap))
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
        public IActionResult UpdateEndereco(int enderecoId, [FromBody] Endereco updatedEndereco)
        {
            if ( updatedEndereco== null)
                return BadRequest(ModelState);

            if (enderecoId != updatedEndereco.Id)
                return BadRequest(ModelState);

            if (!_enderecoRepository.EnderecoExists(enderecoId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var enderecoMap = _mapper.Map<Endereco>(updatedEndereco);

            if (!_enderecoRepository.UpdateEndereco(enderecoMap))
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
            if (!_enderecoRepository.EnderecoExists(enderecoId))
                return NotFound();

            var enderecoToDelete = _enderecoRepository.GetEndereco(enderecoId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_enderecoRepository.DeleteEndereco(enderecoToDelete))
            {
                ModelState.AddModelError("", "Algo deu errado ao deletar");
            }

            return NoContent();

        }



    }
}
