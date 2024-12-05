using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/vipclient")]  
[ApiController]
public class VipClientController : ControllerBase 
{
    private readonly IVipClientService _vipClientService; 

    public VipClientController(IVipClientService vipClientService) //nombre del parámetro y servicio
    {
        _vipClientService = vipClientService;
    }

    [HttpGet]
    [Authorize(Policy = "VipClientOrProfessionalOrSuperAdmin")] 
    public IActionResult GetAllVipClients()  
    {
        var response = _vipClientService.GetAllVipClient(); //método referenciando al servicio

        if (response.Count is 0)
        {
            return NotFound("Vip Client not found");  
        }

        return Ok(response);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "VipClientOrProfessionalOrSuperAdmin")] 
    public ActionResult<VipClientResponse?> GetVipClientById([FromRoute] int id)  
    {
        var response = _vipClientService.GetVipClientById(id);  

        if (response is null)
        {
            return NotFound("Vip Client not found"); 
        }

        return Ok(response);
    }

    [HttpPost]
    public IActionResult CreateVipClient([FromBody] VipClientRequest vipClient) 
    {
        _vipClientService.CreateVipClient(vipClient);  
        return Ok("Usuario creado con éxito"); 
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "VipClientOrSuperAdmin")] 
    public ActionResult<bool> UpdateVipClient([FromRoute] int id, [FromBody] VipClientRequest vipClient) 
    {
        return Ok(_vipClientService.UpdateVipClient(id, vipClient));  
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "VipClientOrSuperAdmin")]
    public ActionResult<bool> DeleteVipClient([FromRoute] int id)  
    {
        return Ok(_vipClientService.DeleteVipClient(id)); 
    }
}
