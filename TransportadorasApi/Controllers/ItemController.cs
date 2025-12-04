using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using TransportadorasApi.Dto;
using TransportadorasApi.Interfaces.IService;
using TransportadorasApi.Model;

namespace TransportadorasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;
        public ItemController(IItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Item>))]
        public IActionResult GetItens()
        {
            var itens = _mapper.Map<List<ItemDto>>(GetItens());
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(itens);
        }

        [HttpGet("{itemId}")]
        [ProducesResponseType(200, Type = typeof(Item))]

        public IActionResult GetItem(int itemId)
        {
            if (!_itemService.ItemExists(itemId))
                return NotFound();

            var item = _mapper.Map<ItemDto>(_itemService.GetItem(itemId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(item);

        }

        [HttpGet("pedidos/{itemId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pedido>))]
        [ProducesResponseType(400)]
        public IActionResult GetPedidosByItem(int itemId)
        {
            if (!_itemService.ItemExists(itemId))
                return NotFound();

            var pedidos = _mapper.Map<List<Pedido>>(_itemService.GetPedidosByItem(itemId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pedidos);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateItem([FromBody] ItemDto itemCreate) 
        {
            if (itemCreate == null)
                return BadRequest(ModelState);

            if(_itemService.ItemExists(itemCreate.Id))
            {
                ModelState.AddModelError("", "Item já cadastrado.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemMap = _mapper.Map<Item>(itemCreate);

            if(!_itemService.CreateItem(itemMap))
            {
                ModelState.AddModelError("", "Algo deu errado ao criar o item.");
                return StatusCode(500, ModelState);

            }

            return Ok("Criado com sucesso.");            
        }

        [HttpPut("{itemId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateItem(int itemId, [FromBody]ItemDto updatedItem)
        {
            if (updatedItem == null)
                return BadRequest(ModelState);

            if (itemId != updatedItem.Id)
                return BadRequest(ModelState);

            if (_itemService.ItemExists(itemId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemMap = _mapper.Map<Item>(updatedItem);

            if(!_itemService.UpdateItem(itemMap))
            {
                ModelState.AddModelError("","Algo deu errado ao atualizar o item.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{itemId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        
        public IActionResult DeleteItem(int itemId)
        {
            if (!_itemService.ItemExists(itemId))
                return NotFound();

            var ItemToDelete = _itemService.GetItem(itemId);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_itemService.DeleteItem(ItemToDelete))
            {
                ModelState.AddModelError("", "Algo deu errado ao deletar item");
            }

            return NoContent();
        }


    }
}
