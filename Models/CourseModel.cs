using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace discgolf_duels.Models
{

    /* 
    tabell/modell för varje bana
    */
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }

        [Required]
        public required string Par { get; set; }

        [Display(Name = "Course")]
        [Required]
        [StringLength(150)]
        public required string CourseName { get; set; }
    }
}