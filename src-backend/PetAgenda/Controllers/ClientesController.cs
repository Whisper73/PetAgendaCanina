using Microsoft.AspNetCore.Mvc;
using Models;
using PetAgenda.Abstractions.Repositories;

namespace PetAgenda.Controllers {

    [Route("Clientes")]
    [ApiController]
    public sealed class ClientesController : ControllerBase {

        private readonly IRepository _Repo;

        public ClientesController(IRepository Repo) {
            _Repo = Repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>?>> GetAll() {

            IEnumerable<Cliente>? clientes = await _Repo.Clientes.GetAll();

            if (clientes == null) {
                return NotFound();
            }

            return Ok(clientes);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetById(int id) {

            Cliente? cliente = await _Repo.Clientes.GetById(id);

            if (cliente == null) {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cliente cliente) {

            if (cliente == null) {
                return BadRequest();
            }

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            bool wasCreated = await _Repo.Clientes.Insert(cliente);

            return Created("Creado", wasCreated);

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Cliente cliente) {

            if (cliente == null) {
                return BadRequest();
            }

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            bool newCliente = await _Repo.Clientes.Update(cliente);

            return Created("Actualizado", newCliente);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {

            bool wasDeleted = await _Repo.Clientes.Delete(id);

            if (!wasDeleted) {
                return NotFound();
            }

            return Ok($"Cliente {id} was Deleted");

        }

    }

}
