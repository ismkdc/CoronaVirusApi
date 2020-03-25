using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaVirusApi.Models.DTO
{
    public class InfectedDTO
    {
        [Required(ErrorMessage = "Lütfen bir ad giriniz!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lütfen bir soyad giriniz!")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Lütfen bir cinsiyet giriniz!")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Lütfen bir yaş giriniz!")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Lütfen bir lokasyon giriniz!")]
        public string Location { get; set; }
    }
}
