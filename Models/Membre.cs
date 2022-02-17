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

        public List<Publication> Publications { get; set; }

        public Membre(string email, string nom, string prenom)
        {
            Email = email;
            Nom = nom;
            Prenom = prenom;
        }
    }
}
