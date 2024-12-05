using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class MeetingRequest
    {
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public int ProfessionalId { get; set; }
    }
}
