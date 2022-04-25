using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Tables
{
    public class RequestStatus:BaseEntity
    {
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public string Description { get; set; }
    }
}
