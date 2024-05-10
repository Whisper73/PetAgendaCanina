using Abstractions.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace PetAgenda.Controllers {

    [Route("Empleados")]
    [ApiController]
    public class EmpleadosController : ControllerBase {

        private readonly IEmpleadoRepository _empleadoRepo;

        public EmpleadosController(IEmpleadoRepository empleadoRepo) {
            _empleadoRepo = empleadoRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>?>> GetAll() {

            IEnumerable<Empleado>? empleados = await _empleadoRepo.GetAll();

            if (empleados == null) {
                return NotFound();
            }

            return Ok(empleados);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetById(int id) {

            Empleado? empleado = await _empleadoRepo.GetById(id);

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

            bool wasCreated = await _empleadoRepo.Insert(empleado);

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

            var newEmpleado = await _empleadoRepo.Update(empleado);

            return Created("Creado", newEmpleado);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {

            bool wasDeleted = await _empleadoRepo.Delete(id);

            if (!wasDeleted) {
                return NotFound();
            }

            return NoContent();

        }

    }

}
