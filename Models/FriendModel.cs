/*using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace discgolf_duels.Models
{
    public class Friend
    {
        [StringLength(150)]
        public string UserEmail { get; set; }

        [ForeignKey("UserEmail")]
        public ApplicationUser User { get; set; }

        [StringLength(150)]
        public string FriendEmail { get; set; }

        [ForeignKey("FriendEmail")]
        public ApplicationUser FriendUser { get; set; }
    }
}
*/