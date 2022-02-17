using System.ComponentModel.DataAnnotations;

namespace ProjectFirstSteps.Models
{
    public class Moderateur
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
    }
}
