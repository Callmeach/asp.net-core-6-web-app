using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFirstSteps.Models
{
    public class Membre
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password lenght too weak, 6 characters minimum.")]
        public string Password { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        // Friends list
        [NotMapped]
        public virtual ICollection<MembreMembre>? Friends { get; set; }

        // Invitaion
        [NotMapped]
        public virtual ICollection<Invitation>? Invitations { get; set; }

        // Messages
        [NotMapped]
        public virtual ICollection<Chat>? Chats { get; set; }
    }
}
