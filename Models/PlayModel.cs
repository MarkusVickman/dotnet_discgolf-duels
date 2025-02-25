

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace discgolf_duels.Models {
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
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    
    public bool Active { get; set; }
    
    public int? SessionId { get; set; } // Nullable for cases where SessionId is not needed
}
}