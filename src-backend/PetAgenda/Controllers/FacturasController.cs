using Microsoft.AspNetCore.Mvc;
using Models;
using PetAgenda.Abstractions.Repositories;

namespace PetAgenda.Controllers
{
    [Route("Facturas")]
    [ApiController]
    public sealed class FacturasController : ControllerBase
    {
        private readonly IRepository _Repo;

        public FacturasController(IRepository Repo)
        {
            _Repo = Repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>?>> GetAll()
        {
            IEnumerable<Factura>? facturas = await _Repo.Facturas.GetAll();

            if (facturas == null)
            {
                return NotFound();
            }

            return Ok(facturas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> GetById(int id)
        {
            Factura? factura = await _Repo.Facturas.GetById(id);

            if (factura == null)
            {
                return NotFound();
            }

            return Ok(factura);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Factura factura)
        {
            if (factura == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool wasCreated = await _Repo.Facturas.Insert(factura);

            return Created("Creada", wasCreated);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Factura factura)
        {
            if (factura == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool newFactura = await _Repo.Facturas.Update(factura);

            return Created("Actualizada", newFactura);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            bool wasDeleted = await _Repo.Facturas.Delete(id);

            if (!wasDeleted)
            {
                return NotFound();
            }

            return Ok($"Factura {id} was Deleted");

        }

    }

}
