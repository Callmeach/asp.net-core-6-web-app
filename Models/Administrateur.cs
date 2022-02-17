using System.ComponentModel.DataAnnotations;

namespace ProjectFirstSteps.Models
{
    public class Administrateur
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }    
    }
}
