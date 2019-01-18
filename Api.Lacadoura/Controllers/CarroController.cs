using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Locadoura.Model;
using Api.Locadoura.Persistencia;
using Microsoft.AspNetCore.Mvc;

namespace Api.Locadoura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarroController : ControllerBase
    {
        private readonly LocadouraDAO _locadouraDao;

        public CarroController()
        {
            _locadouraDao = new LocadouraDAO();
        }

        // GET: api/Carro
        [HttpGet]
        public IEnumerable<Carro> Get()
        {
            var lista = _locadouraDao.GetCarros();
            return lista;
        }

        [HttpGet("{id}", Name = "GetCarro")]
        public async Task<ActionResult<Carro>> GetCarro(int id)
        {
            var carro = _locadouraDao.GetCarroById(id);

            if (carro == null)
            {
                return NotFound();
            }

            return carro;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Carro>> PutCarro(long id, Carro carro)
        {
            if (id != carro.Id)
            {
                return BadRequest();
            }

            _locadouraDao.AtualizarCarro(carro);

            return carro;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Carro>> DeleteCarro(int id)
        {
            var carro = _locadouraDao.GetCarroById(id);
            if (carro == null)
            {
                return NotFound();
            }

            _locadouraDao.ExcluirCarro(c => c.Id == id);

            return carro;
        }

        [HttpPost]
        public async Task<ActionResult<Carro>> PostCarro(Carro carro)
        {
            _locadouraDao.AddCarro(carro);
            return CreatedAtAction("GetCarro", new { id = carro.Id }, carro);
        }
    }
}
