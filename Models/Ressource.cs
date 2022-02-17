using System.ComponentModel.DataAnnotations;

namespace ProjectFirstSteps.Models
{
    public abstract class Ressource
    {
        public string nomRessource { get; set;}

        public ICollection<Publication> lesPublications { get; set; }
    }
}
