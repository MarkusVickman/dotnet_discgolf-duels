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
        public required int PlayId { get; set; }
        [ForeignKey("PlayId")]
        public Play? Play { get; set; }

        public string? Par { get; set; }
        public int? GroupNr { get; set; }

        [Required]
        public required int PublicUserId { get; set; }
        [ForeignKey("PublicUserId")]
        public PublicUser? PublicUser { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now; // Registreringsdatum
    }
}