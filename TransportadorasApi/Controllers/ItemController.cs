using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
            var itens = _itemService.GetItens();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(itens);
        }

        [HttpGet("{itemId}")]
        [ProducesResponseType(200, Type = typeof(Item))]
        [ProducesResponseType(404)]
        public IActionResult GetItem(int id)
        {
            if (!_itemService.ItemExists(id))
                return NotFound();

            var item = _itemService.GetItem(id);

            return Ok(item);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateItem([FromBody] ItemDto dto)
        {
            if (dto == null)
                return BadRequest(ModelState);

            var item = _mapper.Map<Item>(dto);

            if (!_itemService.CreateItem(dto))
            {
                ModelState.AddModelError("", "Erro ao criar item");
                return StatusCode(500, ModelState);
            }

            return Ok("Item criado com sucesso!");
        }

        [HttpPut("{itemId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateItem(int id, [FromBody] ItemDto dto)
        {
            if (!_itemService.ItemExists(id))
                return NotFound();

            if (!_itemService.UpdateItem(id, dto))
            {
                ModelState.AddModelError("", "Erro ao atualizar item");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{itemId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteItem(int id)
        {
            if (!_itemService.ItemExists(id))
                return NotFound();

            if (!_itemService.DeleteItem(id))
            {
                ModelState.AddModelError("", "Erro ao deletar item");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
