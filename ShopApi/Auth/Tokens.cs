using System.ComponentModel.DataAnnotations;

namespace Auth
{
    public class Tokens
    {
        [Required]
        public string Access_Token { get; set; } = null!;

        [Required]
        public string Refresh_Token { get; set; } = null!;
    }
}
