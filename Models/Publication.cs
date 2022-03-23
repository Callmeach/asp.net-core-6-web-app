using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFirstSteps.Models
{
    public class Publication
    {
        public int Id { get; set; }

        [Required]
        public DateTime dateDePublication { get; set; }
        
        [Required]
        public Ressource Ressource { get; set; }
        
        [Required]
        [Column("MemberMail")]
        public Membre Membre { get; set; }
    }
}
