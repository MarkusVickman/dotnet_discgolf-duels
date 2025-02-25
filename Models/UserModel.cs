using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace discgolf_duels.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string DisplayName { get; set; }

        public int? PDGA_Nr { get; set; }

        [StringLength(300)]
        public string PictureURL { get; set; }

    }
}
