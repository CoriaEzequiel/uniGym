using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class VipClient : User
    {
        public List<Meeting> Meetings { get; set; } = new List<Meeting>();
        public VipClient()
        {
            TypeUser = "VipClient";
        }
    }
}
