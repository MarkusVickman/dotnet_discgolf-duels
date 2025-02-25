using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace discgolf_duels.Models{
public class Course
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CourseId { get; set; }
    
    [Required]
    public int Par { get; set; }
    
    [StringLength(150)]
    public string CourseName { get; set; }
}
}