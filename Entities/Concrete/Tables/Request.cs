using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Tables
{
    public class Request : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int PriorityId { get; set; }
        public int RequestTypeId { get; set; }
    }
}
