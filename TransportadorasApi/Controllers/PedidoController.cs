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

        public PedidoController(IPedidoService pedidoService, IMapper mapper)
        {
            _pedidoService = pedidoService;
            _mapper = mapper;
        }

      
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pedido>))]
        public IActionResult GetPedidos()
        {
            var pedidos = _pedidoService.GetPedidos();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pedidos);
        }

      
        [HttpGet("{pedidoId}")]
        [ProducesResponseType(200, Type = typeof(Pedido))]
        [ProducesResponseType(404)]
        public IActionResult GetPedido(int pedidoId)
        {
            if (!_pedidoService.PedidoExists(pedidoId))
                return NotFound();

            var pedido = _pedidoService.GetPedido(pedidoId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pedido);
        }

       
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePedido([FromBody] PedidoCreateDto pedidoCreate)
        {
            if (pedidoCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool sucesso = _pedidoService.CreatePedido(
                pedidoCreate.EnderecoOrigemId,
                pedidoCreate.EnderecoDestinoId,
                pedidoCreate.ClienteId,
                pedidoCreate.RotaId,
                pedidoCreate.ItensIds
            );

            if (!sucesso)
            {
                ModelState.AddModelError("", "Algo deu errado ao criar o pedido");
                return StatusCode(500, ModelState);
            }

            return Ok("Pedido criado com sucesso");
        }


        [HttpPut("{pedidoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePedido(int pedidoId, [FromBody] PedidoUpdateDto updatedPedido)
        {
            if (updatedPedido == null)
                return BadRequest(ModelState);

            if (!_pedidoService.PedidoExists(pedidoId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pedido = _pedidoService.GetPedido(pedidoId);
            if (pedido == null)
                return NotFound();

            _mapper.Map(updatedPedido, pedido);

            bool sucesso = _pedidoService.UpdatePedido(pedido, updatedPedido.ItensIds);

            if (!sucesso)
            {
                ModelState.AddModelError("", "Algo deu errado ao atualizar o pedido");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        
        [HttpDelete("{pedidoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePedido(int pedidoId)
        {
            if (!_pedidoService.PedidoExists(pedidoId))
                return NotFound();

            var pedidoToDelete = _pedidoService.GetPedido(pedidoId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_pedidoService.DeletePedido(pedidoId))
            {
                ModelState.AddModelError("", "Algo deu errado ao deletar o pedido");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
