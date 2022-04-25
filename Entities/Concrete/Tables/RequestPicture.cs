using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Tables
{
    public class RequestPicture : BaseEntity
    {
        public int RequestId { get; set; }
        public int PictureId { get; set; }
    }
}
