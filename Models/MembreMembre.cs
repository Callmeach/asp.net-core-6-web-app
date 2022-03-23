using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFirstSteps.Models
{
    public class MembreMembre
    {
        public string MailMembre1 { get; set; }
        public string MailMembre2 { get; set; } 

        public MembreMembre(string Mail1, string Mail2)
        {
            MailMembre1 = Mail1;
            MailMembre2 = Mail2;
        }

        public MembreMembre()
        {
        }
    }
}
