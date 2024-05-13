using Microsoft.AspNetCore.Mvc;
using Models;
using PetAgenda.Abstractions.Repositories;

namespace PetAgenda.Controllers
{
    [Route("Citas")]
    [ApiController]
    public sealed class CitasController : ControllerBase
    {
        private readonly IRepository _Repo;

        public CitasController(IRepository Repo)
        {
            _Repo = Repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cita>?>> GetAll()
        {
            IEnumerable<Cita>? citas = await _Repo.Citas.GetAll();

            if (citas == null)
            {
                return NotFound();
            }

            return Ok(citas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cita>> GetById(int id)
        {
            Cita? cita = await _Repo.Citas.GetById(id);

            if (cita == null)
            {
                return NotFound();
            }

            return Ok(cita);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cita cita)
        {
            if (cita == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool wasCreated = await _Repo.Citas.Insert(cita);

            return Created("Creada", wasCreated);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Cita cita)
        {
            if (cita == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool newCita = await _Repo.Citas.Update(cita);

            return Created("Actualizada", newCita);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            bool wasDeleted = await _Repo.Citas.Delete(id);

            if (!wasDeleted)
            {
                return NotFound();
            }

            return Ok($"Cita {id} was Deleted");
        }
    }
}
