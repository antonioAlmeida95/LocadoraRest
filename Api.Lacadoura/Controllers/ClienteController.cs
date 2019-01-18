using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Locadoura.Model;
using Api.Locadoura.Persistencia;
using Microsoft.AspNetCore.Mvc;

namespace Api.Locadoura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly LocadouraDAO _locadouraDao;

        public ClienteController()
        {
            _locadouraDao = new LocadouraDAO();
        }

        // GET: api/Cliente
        [HttpGet]
        public IEnumerable<Cliente> Get()
        {
            var lista = _locadouraDao.GetClientes();
            return lista;
        }

        // GET: api/Cliente/5
        [HttpGet("{id}", Name = "GetCliente")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = _locadouraDao.GetClienteById(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Cliente>> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _locadouraDao.AtualizarCliente(cliente);

            return cliente;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Cliente>> DeleteCliente(int id)
        {
            var cliente = _locadouraDao.GetClienteById(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _locadouraDao.ExcluirCliente(c => c.Id == id);

            return cliente;
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> PostTodoItem(Cliente cliente)
        {
            _locadouraDao.AddCliente(cliente);
            return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
        }
    }
}
