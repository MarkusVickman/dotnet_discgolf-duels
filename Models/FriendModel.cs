
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace discgolf_duels.Models
{
    public class Friend
    {
        [Key]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int FriendId { get; set; }
        [ForeignKey("FriendId")]
        public User FriendUser { get; set; }
    }
}