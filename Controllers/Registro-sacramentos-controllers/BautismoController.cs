using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.DTOs.DtosRegistroSacramentos;
using SanblasBackend.Models.EntitiesRegistroSacramentos;

namespace SanblasBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BautismoController : ControllerBase
    {
        private readonly GlobalContex _context;

        public BautismoController(GlobalContex context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BautismoDto>>> GetBautismos()
        {
            var bautismos = await _context.Bautismos
                .Select(b => new BautismoDto
                {
                    Id = b.Id,
                    Nombre = b.Nombre,
                    Cedula = b.Cedula,
                    PrimerApellido = b.PrimerApellido,
                    SegundoApellido = b.SegundoApellido,
                    NombreParroquia = b.NombreParroquia,
                    FechaBautismo = b.FechaBautismo.ToString("yyyy-MM-dd"),
                    AnnioBautismo = b.AnnioBautismo,
                    Prebispero = b.Prebispero,
                    FechaNacimiento = b.FechaNacimiento.ToString("yyyy-MM-dd"),
                    HoraNacimiento = b.HoraNacimiento.ToString(@"hh\:mm"),
                    NombreAbuelosPaternos = b.NombreAbuelosPaternos,
                    NombreAbuelosMaternos = b.NombreAbuelosMaternos
                })
                .ToListAsync();

            return Ok(bautismos);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<BautismoDto>> GetBautismo(int id)
        {
            var bautismo = await _context.Bautismos.FindAsync(id);

            if (bautismo == null)
                return NotFound();

            var dto = new BautismoDto
            {
                Id = bautismo.Id,
                Nombre = bautismo.Nombre,
                Cedula = bautismo.Cedula,
                PrimerApellido = bautismo.PrimerApellido,
                SegundoApellido = bautismo.SegundoApellido,
                NombreParroquia = bautismo.NombreParroquia,
                FechaBautismo = bautismo.FechaBautismo.ToString("yyyy-MM-dd"),
                AnnioBautismo = bautismo.AnnioBautismo,
                Prebispero = bautismo.Prebispero,
                FechaNacimiento = bautismo.FechaNacimiento.ToString("yyyy-MM-dd"),
                HoraNacimiento = bautismo.HoraNacimiento.ToString(@"hh\:mm"),
                NombreAbuelosPaternos = bautismo.NombreAbuelosPaternos,
                NombreAbuelosMaternos = bautismo.NombreAbuelosMaternos
            };

            return Ok(dto);
        }

       
        [HttpPost]
        public async Task<ActionResult<BautismoDto>> CreateBautismo([FromBody] BautismoDto dto)
        {
            var bautismo = new Bautismo
            {
                Nombre = dto.Nombre,
                Cedula = dto.Cedula,
                PrimerApellido = dto.PrimerApellido,
                SegundoApellido = dto.SegundoApellido,
                NombreParroquia = dto.NombreParroquia,
                FechaBautismo = DateTime.Parse(dto.FechaBautismo),
                AnnioBautismo = dto.AnnioBautismo,
                Prebispero = dto.Prebispero,
                FechaNacimiento = DateTime.Parse(dto.FechaNacimiento),
                HoraNacimiento = TimeSpan.Parse(dto.HoraNacimiento),
                NombreAbuelosPaternos = dto.NombreAbuelosPaternos,
                NombreAbuelosMaternos = dto.NombreAbuelosMaternos
            };

            _context.Bautismos.Add(bautismo);
            await _context.SaveChangesAsync();

            dto.Id = bautismo.Id;

            return CreatedAtAction(nameof(GetBautismo), new { id = bautismo.Id }, dto);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBautismo(int id, [FromBody] BautismoDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID de la URL no coincide con el ID del cuerpo");

            var bautismo = await _context.Bautismos.FindAsync(id);
            if (bautismo == null)
                return NotFound();

            bautismo.Nombre = dto.Nombre;
            bautismo.Cedula = dto.Cedula;
            bautismo.PrimerApellido = dto.PrimerApellido;
            bautismo.SegundoApellido = dto.SegundoApellido;
            bautismo.NombreParroquia = dto.NombreParroquia;
            bautismo.FechaBautismo = DateTime.Parse(dto.FechaBautismo);
            bautismo.AnnioBautismo = dto.AnnioBautismo;
            bautismo.Prebispero = dto.Prebispero;
            bautismo.FechaNacimiento = DateTime.Parse(dto.FechaNacimiento);
            bautismo.HoraNacimiento = TimeSpan.Parse(dto.HoraNacimiento);
            bautismo.NombreAbuelosPaternos = dto.NombreAbuelosPaternos;
            bautismo.NombreAbuelosMaternos = dto.NombreAbuelosMaternos;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBautismo(int id)
        {
            var bautismo = await _context.Bautismos.FindAsync(id);
            if (bautismo == null)
                return NotFound();

            _context.Bautismos.Remove(bautismo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}