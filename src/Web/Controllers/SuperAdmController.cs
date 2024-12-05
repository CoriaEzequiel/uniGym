using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/superAdm")]
[ApiController]
public class SuperAdmController : ControllerBase
{
    private readonly ISuperAdm _superAdmService;

    public SuperAdmController(ISuperAdm superAdmService)
    {
        _superAdmService = superAdmService;
    }

    

    [HttpGet("{id}")]
    [Authorize(Policy = "SuperAdmOnly")]
    public ActionResult<SuperAdmResponse?> GetSuperAdmById([FromRoute] int id)
    {
        var response = _superAdmService.GetSuperAdminById(id);

        if (response is null)
        {
            return NotFound("SuperAdm not found");
        }

        return Ok(response);
    }

    [HttpPost]
    public IActionResult CreateSuperAdm([FromBody] SuperAdmRequest superAdm)
    {
        _superAdmService.CreateSuperAdm(superAdm);
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "SuperAdmOnly")]
    public ActionResult<bool> UpdateSuperAdm([FromRoute] int id, [FromBody] SuperAdmRequest superAdm)
    {
        return Ok(_superAdmService.UpdateSuperAdm(id, superAdm));
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "SuperAdmOnly")]
    public ActionResult<bool> DeleteSuperAdm([FromRoute] int id)
    {
        return Ok(_superAdmService.DeleteSuperAdm(id));
    }
}
