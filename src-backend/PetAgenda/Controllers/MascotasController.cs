using Microsoft.AspNetCore.Mvc;
using Models;
using PetAgenda.Abstractions.Repositories;

namespace PetAgenda.Controllers
{

    [Route("Mascota")]
    [ApiController]
    public sealed class MascotasController : ControllerBase
    {

        private readonly IRepository _Repo;

        public MascotasController(IRepository Repo)
        {
            _Repo = Repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mascota>?>> GetAll()
        {

            IEnumerable<Mascota>? mascotas = await _Repo.Mascotas.GetAll();

            if (mascotas == null)
            {
                return NotFound();
            }

            return Ok(mascotas);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Mascota>> GetById(int id)
        {

            Mascota? mascota = await _Repo.Mascotas.GetById(id);

            if (mascota == null)
            {
                return NotFound();
            }

            return Ok(mascota);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Mascota mascota)
        {

            if (mascota == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool wasCreated = await _Repo.Mascotas.Insert(mascota);

            return Created("Creado", wasCreated);

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Mascota mascota)
        {

            if (mascota == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newMascota = await _Repo.Mascotas.Update(mascota);

            return Created("Creado", newMascota);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {

            bool wasDeleted = await _Repo.Mascotas.Delete(id);

            if (!wasDeleted)
            {
                return NotFound();
            }

            return Ok($"Empleado {id} was Deleted");

        }

    }

}
