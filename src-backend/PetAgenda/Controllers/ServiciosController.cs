using Microsoft.AspNetCore.Mvc;
using Models;
using PetAgenda.Abstractions.Repositories;

namespace PetAgenda.Controllers
{
    [Route("Servicios")]
    [ApiController]
    public sealed class ServiciosController : ControllerBase
    {
        private readonly IRepository _Repo;

        public ServiciosController(IRepository Repo)
        {
            _Repo = Repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servicio>?>> GetAll()
        {
            IEnumerable<Servicio>? servicios = await _Repo.Servicios.GetAll();

            if (servicios == null)
            {
                return NotFound();
            }

            return Ok(servicios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Servicio>> GetById(int id)
        {
            Servicio? servicio = await _Repo.Servicios.GetById(id);

            if (servicio == null)
            {
                return NotFound();
            }

            return Ok(servicio);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Servicio servicio)
        {
            if (servicio == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool wasCreated = await _Repo.Servicios.Insert(servicio);

            return Created("Creado", wasCreated);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Servicio servicio)
        {
            if (servicio == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool newServicio = await _Repo.Servicios.Update(servicio);

            return Created("Actualizado", newServicio);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            bool wasDeleted = await _Repo.Servicios.Delete(id);

            if (!wasDeleted)
            {
                return NotFound();
            }

            return Ok($"Servicio {id} was Deleted");
        }
    }
}
