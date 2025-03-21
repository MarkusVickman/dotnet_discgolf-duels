using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace discgolf_duels.Models
{

    /* 
    Modell och tabell för att hantera registreringar till tävlingar. 
    Har FK från publicuser(usermodel) och competition. 
     */
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
        [Display(Name = "User")]
        public PublicUser? PublicUser { get; set; }

        [Display(Name = "Registration date")]
        public DateTime RegisterDate { get; set; } = DateTime.Now; // Registreringsdatum
    }
}
