

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace discgolf_duels.Models
{
    public class Play
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayId { get; set; }

        public int? Par { get; set; }

        public int? GroupNr { get; set; }

        public int? CompetitionId { get; set; }
        [ForeignKey("CompetitionId")]
        public Competition Competition { get; set; }

        [Required]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        [Required]
        [StringLength(150)]
        public string UserEmail { get; set; } // Ändra typen till string och använd Email som främmande nyckel
        [ForeignKey("UserEmail")]
        public ApplicationUser User { get; set; }
        public bool Active { get; set; }

        public int? SessionId { get; set; } // Nullable for cases where SessionId is not needed
    }
}