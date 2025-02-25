using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace discgolf_duels.Models
{
public class Play
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PlayId { get; set; }

    public int? CompetitionId { get; set; }
    [ForeignKey("CompetitionId")]
    public Competition Competition { get; set; }

    [Required]
    public int CourseId { get; set; }
    [ForeignKey("CourseId")]
    public Course Course { get; set; }

    public bool Active { get; set; }
}

}