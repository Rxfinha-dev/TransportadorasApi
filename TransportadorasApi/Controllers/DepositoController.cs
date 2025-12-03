using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using TransportadorasApi.Interface;
using TransportadorasApi.Model;

namespace TransportadorasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositoController : Controller
    {
        private readonly IDepositoRepository _depositoRepository;
        private readonly IMapper _mapper;
        public DepositoController(IDepositoRepository depositoRepository, IMapper mapper) 
        {
            _depositoRepository = depositoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Deposito>))]
        public IActionResult GetDepositos()
        {
            var depositos = _depositoRepository.GetDepositos();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(depositos);
        }

        [HttpGet("{depositoId}")]
        [ProducesResponseType(200, Type = typeof(Deposito))]
        [ProducesResponseType(400)]
        public IActionResult GetDeposito(int depositoId)
        {
            if (!_depositoRepository.DepositoExists(depositoId))
                return NotFound();

            var deposito = _depositoRepository.GetDeposito(depositoId);

            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            return Ok(deposito);
        }
        [HttpGet("{enderecoId}/deposito")]
        [ProducesResponseType(200, Type = typeof(Deposito))]
        [ProducesResponseType(400)]
        public IActionResult GetDepositoByEndereco(int enderecoId)
        {
            var deposito = _depositoRepository.GetDepositoByEndereco(enderecoId);



            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(deposito);
        }

        [HttpGet("{depositoId}/endereco")]
        [ProducesResponseType(200, Type = typeof(Deposito))]
        [ProducesResponseType(400)]

        public IActionResult GetDepositoEndereco(int depositoId)
        {
            if (!_depositoRepository.DepositoExists(depositoId))
                return NotFound();

            var endereco = _depositoRepository.getDepositoEndereco(depositoId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(endereco);


        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateDeposito([FromBody] Deposito depositoCreate)
        {
            if (depositoCreate == null)
                return BadRequest(ModelState);

            var deposito = _depositoRepository.GetDepositos()
                .Where(d=>d.Localizacao == depositoCreate.Localizacao).FirstOrDefault();

            if(deposito != null)
            {
                ModelState.AddModelError("", "Deposito Já Existe");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var depositoMap = _mapper.Map<Deposito>(depositoCreate);

            if (!_depositoRepository.CreateDeposito(depositoMap)) 
            {
                ModelState.AddModelError("", "Algo deu errado ao criar o depósito");
                return StatusCode(500, ModelState);
            }

            return Ok("Criado Com Sucesso");
        }

        [HttpPut("{depositoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDeposito(int depositoId, [FromBody] Deposito updatedDeposito)
        {
            if (updatedDeposito == null)
                return BadRequest(ModelState);

            if (depositoId != updatedDeposito.Id)
                return BadRequest(ModelState);

            if (!_depositoRepository.DepositoExists(depositoId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var depositoMap = _mapper.Map<Deposito>(updatedDeposito);

            if (!_depositoRepository.UpdateDeposito(depositoId, depositoMap))
            {
                ModelState.AddModelError("", "Algo deu errado ao Atualizar o depósito");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{depositoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDeposito(int depositoId)
        {
            if (!_depositoRepository.DepositoExists(depositoId))
            {
                return NotFound();
            }

            var depositoToDelete = _depositoRepository.GetDeposito(depositoId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_depositoRepository.DeleteDeposito(depositoToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }





    }
}
