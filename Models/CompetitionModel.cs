using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace discgolf_duels.Models
{
    public class Competition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompetitionId { get; set; }

        public DateTime? CompetitionDate { get; set; }

        [Required]
        public int MaxPlayerCount { get; set; }

        [Required]
        [StringLength(150)]
        public string UserEmail { get; set; } // Ändra typen till string och använd Email som främmande nyckel
        [ForeignKey("UserEmail")]
        public ApplicationUser User { get; set; }

        // Ny lista för att hantera registreringar
        public ICollection<Registration> Registrations { get; set; }
    }
}
