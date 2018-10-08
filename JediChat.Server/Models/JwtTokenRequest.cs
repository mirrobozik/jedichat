using System.ComponentModel.DataAnnotations;

namespace JediChat.Server.Models
{
    public class JwtTokenRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}