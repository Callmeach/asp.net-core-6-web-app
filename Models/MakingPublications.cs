using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFirstSteps.Models
{
    [NotMapped]
    public class MakingPublications
    {
        

        public Message message { get; set; }

        public Lien lien { get; set; }

        public PhotoVideo photoVideo { get; set; }

        public Publication publication { get; set; }

        public MakingPublications()
        {
            message = new Message();
            lien = new Lien();
            photoVideo = new PhotoVideo();
            publication = new Publication();
        }
    }
}
