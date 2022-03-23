using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjectFirstSteps.Models
{
    public abstract class Ressource
    {
        [Column("RessourceID")]
        public int Id { get; set; }

        public string nomRessource { get; set;}

    }
}
