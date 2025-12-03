using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TransportadorasApi.Interfaces.IService;
using TransportadorasApi.Model;

namespace TransportadorasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;      
        private readonly IMapper _mapper;
        public ClienteController(IClienteService clienteService, IMapper mapper, IClienteService clienteRepository)
        {
            _clienteService = clienteService;
            _mapper = mapper;
           ;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Cliente>))]
        public IActionResult GetClientes()
        {
            var clientes = _clienteService.GetClientes();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(clientes);
        }

        [HttpGet("{clienteId}")]
        [ProducesResponseType(200, Type = typeof(Cliente))]
        [ProducesResponseType(400)]
        public IActionResult GetCliente(int clienteId)
        {
            if(!_clienteService.ClienteExists(clienteId))
                return NotFound();

            var cliente = _clienteService.GetCliente(clienteId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);  

            return Ok(cliente);
        }

        [HttpGet("by-cpf/{clienteCpf}")]
        [ProducesResponseType(200, Type = typeof(Cliente))]
        [ProducesResponseType(400)]
        public IActionResult GetClienteByCpf(string clienteCpf)
        {
            if(!_clienteService.ClienteExistsByCpf(clienteCpf))
                return NotFound();

            var cliente = _clienteService.GetCliente(clienteCpf);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(cliente);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCliente([FromBody]Cliente clienteCreate)
        {
            if (clienteCreate == null)
                return BadRequest(ModelState);

            if(_clienteService.ClienteExistsByCpf(clienteCreate.Cpf))
            {
                ModelState.AddModelError("", "Cliente já existe");

                return StatusCode(422, ModelState);
            }
                            

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clienteMap = _mapper.Map<Cliente>(clienteCreate);

            if (!_clienteService.CreateCliente(clienteMap))
            {
                ModelState.AddModelError("", "Algo deu errado ao criar o cliente");
                return StatusCode(500, ModelState);
            }

            return Ok("Criado Com Sucesso");

        }
        [HttpPut("{clienteId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDeposito(int clienteId, [FromBody] Cliente updatedCliente)
        {
            if (updatedCliente == null)
                return BadRequest(ModelState);

            if (clienteId != updatedCliente.Id)
                return BadRequest(ModelState);

            if (!_clienteService.ClienteExists(clienteId))
                return NotFound();

          

            if (!ModelState.IsValid)
                return BadRequest();

            var clienteMap = _mapper.Map<Cliente>(updatedCliente);

            if (!_clienteService.UpdateCliente(clienteId, clienteMap))
            {
                ModelState.AddModelError("", "Algo deu errado ao Atualizar o cliente");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{clienteId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDeposito(int clienteId)
        {
            if (!_clienteService.ClienteExists(clienteId))
            {
                return NotFound();
            }

            var clienteToDelete = _clienteService.GetCliente(clienteId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_clienteService.DeleteCliente(clienteToDelete))
            {
                ModelState.AddModelError("", "Algo deu errado ao deletar o cliente");
            }

            return NoContent();
        }


    }
}
