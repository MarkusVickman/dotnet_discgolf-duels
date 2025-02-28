using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace discgolf_duels.Models
{
    public class Competition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompetitionId { get; set; }

        [Display(Name = "Competition")]
        [Required]
        public required String CompetitionName { get; set; }

        [Required]
        public required int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course? Course { get; set; }

        [Display(Name = "Date")]
        [Required]
        public required DateTime CompetitionDate { get; set; }

        [Display(Name = "Player limit")]
        [Required]
        public required int MaxPlayerCount { get; set; }

        [Required]
        public required int PublicUserId { get; set; }

        [ForeignKey("PublicUserId")]
        [Display(Name = "User")]
        public PublicUser? PublicUser { get; set; }

        // Ny lista för att hantera registreringar
        public ICollection<Registration>? Registrations { get; set; }
    }
}
