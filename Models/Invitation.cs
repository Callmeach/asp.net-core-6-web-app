using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFirstSteps.Models
{
    public class Invitation
    {
        public int Id { get; set; }
        [Required]
        public string InviterMail { get; set; }
        [NotMapped]
        public Membre Inviter { get; set; }

        [Required]
        public string InvitedMail { get; set; }
        [NotMapped]
        public Membre Invited { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, NullDisplayText = "Non défini")]
        public DateTime? InvDate { get; set; }

        public Invitation(string inviterMail, string invitedMail, DateTime invDate)
        {
            this.InviterMail = inviterMail;
            this.InvitedMail = invitedMail;
            this.InvDate = invDate;
        }
        public Invitation(string inviterMail, string invitedMail)
        {
            this.InviterMail = inviterMail;
            this.InvitedMail = invitedMail;
        }
    }
}
