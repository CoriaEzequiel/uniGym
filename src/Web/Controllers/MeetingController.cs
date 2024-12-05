using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/meeting")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;

        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [HttpGet]
        [Authorize(Policy = "VipClientOrProfessorOrSuperAdmin")]
        public IActionResult GetAllMeetings()
        {
            var response = _meetingService.GetAllMeetings();

            if (response.Count == 0)
            {
                return NotFound("No se encuentran reuniones");
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "VipClientOrProfessorOrSuperAdmin")]
        public ActionResult<MeetingResponse?> GetMeetingById([FromRoute] int id)
        {
            var response = _meetingService.GetMeetingById(id);

            if (response is null)
            {
                return NotFound("Reunión no encontrada");
            }

            return Ok(response);
        }

        [HttpGet("professor/{professorId}")]
        [Authorize(Policy = "VipClientOrProfessorOrSuperAdmin")]
        public IActionResult GetMeetingsByProfessor([FromRoute] int professorId)
        {
            var response = _meetingService.GetMeetingsByProfessor(professorId);
            return Ok(response);
        }

        [HttpGet("vipclient/{vipClientId}")]
        [Authorize(Policy = "VipClientOrProfessorOrSuperAdmin")]
        public IActionResult GetMeetingsByVipClient([FromRoute] int vipClientId)
        {
            var response = _meetingService.GetMeetingsByVipClient(vipClientId);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = "VipClientOnly")]
        public IActionResult CreateMeeting([FromBody] MeetingRequest meeting)
        {
            Console.WriteLine("Fecha y hora recibida: " + meeting.Date);

            if (meeting.Date == DateTime.MinValue)
            {
                Console.WriteLine("Error: La fecha no se ha recibido correctamente.");
                return BadRequest("Formato de fecha incorrecto");
            }

            _meetingService.CreateMeeting(meeting);
            return Ok("Reunión creada con éxito");
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "VipClientOrProfessorOrSuperAdmin")]
        public ActionResult<bool> UpdateMeeting([FromRoute] int id, [FromBody] MeetingRequest meeting)
        {
            return Ok(_meetingService.UpdateMeeting(id, meeting));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "VipClientOrProfessorOrSuperAdmin")]
        public ActionResult<bool> DeleteMeeting([FromRoute] int id)
        {
            return Ok(_meetingService.DeleteMeeting(id));
        }
    }
}
