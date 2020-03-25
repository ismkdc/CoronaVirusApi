using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaVirusApi.Models
{
    public class Notification : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Content { get; set; }
    }
}
