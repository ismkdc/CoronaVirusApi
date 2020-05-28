using System;

namespace CoronaVirusApi.Models
{
    public class Infected : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
    }
}
