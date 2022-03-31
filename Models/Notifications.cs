using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFirstSteps.Models
{
    public class Notifications
    {
        //L'objectif de cette classe est d'informer les utilisateurs par notification
        //que leurs publication a été signalée, ou qu'un membre a accepté leur invitation,
        //ou encore que leur inscription a été validée.

        public int Id { get; set; }

        public string Contenu { get; set; }

        [ForeignKey("Membre")]
        public string MembreEmail { get; set; }
        public Membre Membre { get; set; }

        public PersonalizedClass? PersonalizedClass { get; set; }

        public bool IsAdvertisement {get; set;} = false;
        
    }
}
