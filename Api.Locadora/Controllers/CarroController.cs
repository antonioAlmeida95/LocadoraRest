using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Locadora.Models;
using Api.Locadora.Persistencia;
using Microsoft.AspNetCore.Mvc;

namespace Api.Locadora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarroController : ControllerBase
    {
        private readonly LocadoraDAO _locadoraDao;

        public CarroController()
        {
            _locadoraDao = new LocadoraDAO();
        }

        // GET: api/Carro
        [HttpGet]
        public IEnumerable<Carro> Get()
        {
            var lista = _locadoraDao.GetCarros();
            return lista;
        }

        [HttpGet("{id}", Name = "GetCarro")]
        public ActionResult<Carro> GetCarro(int id)
        {
            var carro = _locadoraDao.GetCarroById(id);

            if (carro == null)
            {
                return NotFound();
            }

            return carro;
        }

        [HttpPut("{id}")]
        public ActionResult<Carro> PutCarro(long id, Carro carro)
        {
            if (id != carro.Id)
            {
                return BadRequest();
            }

            _locadoraDao.AtualizarCarro(carro);

            return carro;
        }

        [HttpDelete("{id}")]
        public ActionResult<Carro> DeleteCarro(int id)
        {
            var carro = _locadoraDao.GetCarroById(id);
            if (carro == null)
            {
                return NotFound();
            }

            _locadoraDao.ExcluirCarro(c => c.Id == id);

            return carro;
        }

        [HttpPost]
        public ActionResult<Carro> PostCarro(Carro carro)
        {
            _locadoraDao.AddCarro(carro);
            return CreatedAtAction("GetCarro", new { id = carro.Id }, carro);
        }
    }
}
