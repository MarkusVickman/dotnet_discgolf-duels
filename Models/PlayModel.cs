using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace discgolf_duels.Models
{
    /* 
    denna tabell/modell används som grund för varje playing som startas. Den skapas automatiskt för varje bana samt vid start av en tävling
    Har FK från competition samt från course
     */
    public class Play
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayId { get; set; }

        public int? CompetitionId { get; set; }
        [ForeignKey("CompetitionId")]
        public Competition? Competition { get; set; }

        [Required]
        public required int CourseId { get; set; }
        [ForeignKey("CourseId")]
        [Display(Name = "Course")]
        public Course? Course { get; set; }

        public bool Active { get; set; } = false;
    }

}