using System.ComponentModel.DataAnnotations;

namespace MonezyAPI.Models
{
    public class Users
    {
        [Key]
        public int IdUser { get; set; }
        public string? NameUser { get; set; }
        public string? SurnameUser { get; set; }

    }
}
