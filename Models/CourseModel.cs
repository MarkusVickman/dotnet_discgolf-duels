using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace discgolf_duels.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }

        [Required]
        public int Par { get; set; }

        [Required]
        [StringLength(150)]
        public required string CourseName { get; set; }
    }
}