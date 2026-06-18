using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SanblasBackend.DTOs;
using SanblasBackend.Services;

namespace SanblasBackend.Controllers;

[ApiController]
[Route("api/landing")]
public class LandingController : ControllerBase
{
    private readonly ILandingContentService _landingContentService;

    public LandingController(ILandingContentService landingContentService)
    {
        _landingContentService = landingContentService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var sections = await _landingContentService.GetAllSectionsAsync();
        return Ok(sections);
    }

    [HttpGet("{sectionKey}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByKey(string sectionKey)
    {
        var section = await _landingContentService.GetSectionAsync(sectionKey);

        if (section is null)
            return NotFound(new { mensaje = "Sección no encontrada." });

        return Ok(section);
    }

    [HttpPut("{sectionKey}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(string sectionKey, [FromBody] UpdateLandingSectionRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _landingContentService.UpdateSectionAsync(sectionKey, request.Data);

        if (updated is null)
            return NotFound(new { mensaje = "Sección no encontrada." });

        return Ok(updated);
    }
}
