using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TransportadorasApi.Dto;
using TransportadorasApi.Interfaces.IService;
using TransportadorasApi.Model;

namespace TransportadorasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        private readonly IMapper _mapper;

        public PedidoController(IPedidoService service, IMapper mapper)
        {
            _pedidoService = service;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PedidoDto>))]
        [ProducesResponseType(500)]
        public IActionResult GetPedidos()
        {
            var pedidos = _mapper.Map<List<PedidoDto>>(_pedidoService.GetPedidos());
            return Ok(pedidos);
        }

        [HttpGet("{pedidoId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetPedido(int pedidoId)
        {
            if (!_pedidoService.PedidoExists(pedidoId))
                return NotFound();

            var pedido = _mapper.Map<PedidoDto>(_pedidoService.GetPedido(pedidoId));
            return Ok(pedido);
        }
        [HttpGet("itens/{pedidoId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Item>))]
        [ProducesResponseType(400)]
        public IActionResult GetItensByPedido(int pedidoId)
        {
            if (!_pedidoService.PedidoExists(pedidoId))
                return NotFound();

            var itens = _mapper.Map<List<ItemDto>>(_pedidoService.GetItensByPedido(pedidoId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(itens);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreatePedido([FromBody] PedidoDto dto)
        {
            var pedido = _mapper.Map<Pedido>(dto);

            if (!_pedidoService.CreatePedido(pedido, dto.ItensIds))
                return StatusCode(500, "Erro ao criar");

            return Ok("Criado com sucesso");
        }

        [HttpPut("{pedidoId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePedido(int pedidoId, [FromBody] PedidoDto dto)
        {
            if (pedidoId != dto.Id)
                return BadRequest();

            if (!_pedidoService.PedidoExists(pedidoId))
                return NotFound();

            var pedido = _mapper.Map<Pedido>(dto);

            if (!_pedidoService.UpdatePedido(pedido, dto.ItensIds))
                return StatusCode(500, "Erro ao atualizar");

            return NoContent();
        }

        [HttpDelete("{pedidoId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePedido(int pedidoId)
        {
            if (!_pedidoService.PedidoExists(pedidoId))
                return NotFound();

            if (!_pedidoService.DeletePedido(pedidoId))
                return StatusCode(500, "Erro ao remover");

            return NoContent();
        }
    }
}
