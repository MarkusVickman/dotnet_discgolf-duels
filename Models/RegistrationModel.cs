using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace discgolf_duels.Models
{
    public class Registration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegistrationId { get; set; }

        [Required]
        public int CompetitionId { get; set; }
        [ForeignKey("CompetitionId")]
        public Competition? Competition { get; set; }

        [Required]
        public required int PublicUserId { get; set; }
        [ForeignKey("PublicUserId")]
        public PublicUser? PublicUser { get; set; }

        public DateTime RegisterDate { get; set; } = DateTime.Now; // Registreringsdatum
    }
}
