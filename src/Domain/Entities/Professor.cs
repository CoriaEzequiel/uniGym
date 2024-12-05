using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Professor : User
    {
        public ProfessorClass PRofessorClass { get; set; }
        public List<Meeting> Meetings { get; set; } = new List<Meeting>();

        public Professor() {
            TypeUser = "Professor";
        }
    }
}
