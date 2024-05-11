using Microsoft.AspNetCore.Mvc;
using Models;
using PetAgenda.Abstractions.Repositories;

namespace PetAgenda.Controllers {

    [Route("Empleados")]
    [ApiController]
    public sealed class EmpleadosController : ControllerBase {

        private readonly IRepository _Repo;

        public EmpleadosController(IRepository Repo) {
            _Repo = Repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>?>> GetAll() {

            IEnumerable<Empleado>? empleados = await _Repo.Empleados.GetAll();

            if (empleados == null) {
                return NotFound();
            }

            return Ok(empleados);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetById(int id) {

            Empleado? empleado = await _Repo.Empleados.GetById(id);

            if (empleado == null) {
                return NotFound();
            }

            return Ok(empleado);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Empleado empleado) {

            if (empleado == null) {
                return BadRequest();
            }

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            bool wasCreated = await _Repo.Empleados.Insert(empleado);

            return Created("Creado", wasCreated);

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Empleado empleado) {

            if (empleado == null) {
                return BadRequest();
            }

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var newEmpleado = await _Repo.Empleados.Update(empleado);

            return Created("Creado", newEmpleado);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {

            bool wasDeleted = await _Repo.Empleados.Delete(id);

            if (!wasDeleted) {
                return NotFound();
            }

            return Ok($"Empleado {id} was Deleted");

        }

    }

}
