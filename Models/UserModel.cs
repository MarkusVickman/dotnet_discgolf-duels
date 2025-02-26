using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace discgolf_duels.Models
{
    public class PublicUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PublicUserId { get; set; }
        [Required]
        [StringLength(100)]
        public required string DisplayName { get; set; }

        public int? PDGA_Nr { get; set; }

        [StringLength(300)]
        public string? PictureURL { get; set; }

        public required string Id { get; set; }
        [ForeignKey("Id")]
        public IdentityUser? IdentityUser { get; set; }

    }
}
