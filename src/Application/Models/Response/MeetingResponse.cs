using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Response
{
    public class MeetingResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int VipClientId { get; set; }
        public int ProfessorId { get; set; }
    }
}
