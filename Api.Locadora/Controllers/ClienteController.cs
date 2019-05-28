using System.Collections.Generic;
using Api.Locadora.Models;
using Api.Locadora.Persistencia;
using Microsoft.AspNetCore.Mvc;

namespace Api.Locadora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly LocadoraDAO _locadoraDao;

        public ClienteController()
        {
            _locadoraDao = new LocadoraDAO();
        }

        // GET: api/Cliente
        [HttpGet]
        public IEnumerable<Cliente> Get()
        {
            var lista = _locadoraDao.GetClientes();
            return lista;
        }

        // GET: api/Cliente/5
        [HttpGet("{id}", Name = "GetCliente")]
        public ActionResult<Cliente> GetCliente(int id)
        {
            var cliente = _locadoraDao.GetClienteById(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [HttpPut("{id}")]
        public ActionResult<Cliente> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _locadoraDao.AtualizarCliente(cliente);

            return cliente;
        }

        [HttpDelete("{id}")]
        public ActionResult<Cliente> DeleteCliente(int id)
        {
            var cliente = _locadoraDao.GetClienteById(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _locadoraDao.ExcluirCliente(c => c.Id == id);

            return cliente;
        }

        [HttpPost]
        public ActionResult<Cliente> PostTodoItem(Cliente cliente)
        {
            _locadoraDao.AddCliente(cliente);
            return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
        }
    }
}
