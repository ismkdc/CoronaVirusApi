using System;

namespace CoronaVirusApi.Models
{
    public class Notification : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Content { get; set; }
    }
}
