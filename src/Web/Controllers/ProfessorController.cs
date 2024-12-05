using Application.Interfaces;
using Domain.Enum;
using Application.Models.Request;
using Application.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace Web.Controllers;

[Route("api/professor")]  
[ApiController]
public class ProfessorController : ControllerBase
{
    private readonly IProfessorService _professorService;

    public ProfessorController(IProfessorService professorService)
    {
        _professorService = professorService;
    }

    [HttpGet]
    [Authorize(Policy = "VipClientOrProfessorOrSuperAdmin")]
    public IActionResult GetAllProfessors()  
    {
        var response = _professorService.GetAllProfessors();  //servicio.

        if (response.Count is 0)
        {
            return NotFound("Professor not found");
        }

        return Ok(response);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "VipClientOrProfessorOrSuperAdmin")]
    public ActionResult<ProfessorResponse?> GetProfessorById([FromRoute] int id)
    {
        var response = _professorService.GetProfessorById(id);

        if (response is null)
        {
            return NotFound("Professor not found");
        }

        return Ok(response);
    }

    [HttpGet("class/{classType}")]  // Filtra por clase
    [Authorize(Policy = "VipClientOrProfessorOrSuperAdmin")]
    public IActionResult GetProfessorByClass(ProfessorClass classType)  // Uso del Enum.
    {
        var result = _professorService.GetProfessorByClass(classType);
        return Ok(result);
    }

    [HttpPost]
    public IActionResult CreateProfessor([FromBody] ProfessorRequest professor)
    {
        _professorService.CreateProfessor(professor);
        return Ok("Profesor creado con éxito");
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "ProfessorOrSuperAdmin")]
    public ActionResult<bool> UpdateProfessor([FromRoute] int id, [FromBody] ProfessorRequest professor)
    {
        return Ok(_professorService.UpdateProfessor(id, professor));
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "ProfessorOrSuperAdmin")]
    public ActionResult<bool> DeleteProfessor([FromRoute] int id)
    {
        return Ok(_professorService.DeleteProfessor(id));
    }
}
