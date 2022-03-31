using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFirstSteps.Models
{
    public class PersonalizedClass
    {
        //Classe créée pour simplifier la recuperation des publications
        
        public int Id { get; set; }

        public string UserName { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string UserMail { get; set; }

        public DateTime PublicationDate { get; set; }

        public string RessourceName { get; set; }

        public string? Url { get; set; }

        public string? Libelle { get; set; }

        public string? Legende { get; set; }

        public string? Path { get; set; }

        public int? type { get; set; }

    }
}
