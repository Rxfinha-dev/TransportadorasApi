using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using TransportadorasApi.Dto;
using TransportadorasApi.Interfaces.IService;
using TransportadorasApi.Model;

namespace TransportadorasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositoController : Controller
    {
        private readonly IDepositoService _depositoService;
        private readonly IMapper _mapper;
        public DepositoController(IDepositoService depositoRepository, IMapper mapper) 
        {
            _depositoService = depositoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Deposito>))]
        public IActionResult GetDepositos()
        {
            var depositos = _mapper.Map<List<DepositoDto>>(_depositoService.GetDepositos());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(depositos);
        }

        [HttpGet("{depositoId}")]
        [ProducesResponseType(200, Type = typeof(Deposito))]
        [ProducesResponseType(400)]
        public IActionResult GetDeposito(int depositoId)
        {
            if (!_depositoService.DepositoExists(depositoId))
                return NotFound();

            var deposito = _mapper.Map<DepositoDto>(_depositoService.GetDeposito(depositoId));

            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            return Ok(deposito);
        }
        [HttpGet("{enderecoId}/deposito")]
        [ProducesResponseType(200, Type = typeof(Deposito))]
        [ProducesResponseType(400)]
        public IActionResult GetDepositoByEndereco(int enderecoId)
        {
            var deposito = _mapper.Map<DepositoDto>(_depositoService.GetDepositoByEndereco(enderecoId));



            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(deposito);
        }

        [HttpGet("{depositoId}/endereco")]
        [ProducesResponseType(200, Type = typeof(Deposito))]
        [ProducesResponseType(400)]

        public IActionResult GetDepositoEndereco(int depositoId)
        {
            if (!_depositoService.DepositoExists(depositoId))
                return NotFound();

            var endereco = _depositoService.GetDepositoEndereco(depositoId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(endereco);


        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateDeposito([FromBody] DepositoDto depositoCreate)
        {
            if (depositoCreate == null)
                return BadRequest(ModelState);

            var deposito = _depositoService.GetDepositos()
                .Where(d=>d.Localizacao == depositoCreate.Localizacao).FirstOrDefault();

            if(deposito != null)
            {
                ModelState.AddModelError("", "Deposito Já Existe");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var depositoMap = _mapper.Map<Deposito>(depositoCreate);

            if (!_depositoService.CreateDeposito(depositoMap)) 
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
        public IActionResult UpdateDeposito(int depositoId, [FromBody] DepositoDto updatedDeposito)
        {
            if (updatedDeposito == null)
                return BadRequest(ModelState);

            if (depositoId != updatedDeposito.Id)
                return BadRequest(ModelState);

            if (!_depositoService.DepositoExists(depositoId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var depositoMap = _mapper.Map<Deposito>(updatedDeposito);

            if (!_depositoService.UpdateDeposito(depositoId, depositoMap))
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
            if (!_depositoService.DepositoExists(depositoId))
            {
                return NotFound();
            }

            var depositoToDelete = _depositoService.GetDeposito(depositoId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_depositoService.DeleteDeposito(depositoToDelete))
            {
                ModelState.AddModelError("", "Algo deu errado ao deletar o depósito");
            }

            return NoContent();
        }





    }
}
