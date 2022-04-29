using System.ComponentModel.DataAnnotations;

namespace PlatformService.PlatformDomain
{
    public class PlatformWriteDto
    {
        [Key]
        [Required]
        public int PlatformId { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public string Cost { get; set; }
    }
}
