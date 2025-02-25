using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace discgolf_duels.Models
{

    public class Playing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayingId { get; set; }

        [Required]
        public int PlayId { get; set; }
        [ForeignKey("PlayId")]
        public Play Play { get; set; }

        public int? Par { get; set; }
        public int? GroupNr { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}