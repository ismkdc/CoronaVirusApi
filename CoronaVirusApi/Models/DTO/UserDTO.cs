using System.ComponentModel.DataAnnotations;

namespace CoronaVirusApi.Models.DTO
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Lütfen bir kullanıcı adı giriniz!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Lütfen bir parola giriniz!")]
        public string Password { get; set; }
    }
}
