using System.ComponentModel.DataAnnotations;


namespace discgolf_duels.Models {
public class User
{
    [Key]
    [Required]
    [StringLength(150)]
    public string Email { get; set; }
    
    [Required]
    [StringLength(100)]
    public string DisplayName { get; set; }
    
    public int? PDGA_Nr { get; set; }
    
    [StringLength(300)]
    public string PictureURL { get; set; }
    
    [Required]
    [StringLength(20)]
    public string Password { get; set; }
}
}