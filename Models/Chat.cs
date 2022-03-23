using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFirstSteps.Models
{
    public class Chat
    {
        
        public int Id { get; set; }

        public string MailMembre1 { get; set; }
        public string MailMembre2 { get; set; }

        public DateTime MessageDate { get; set; }
        public string Containt { get; set; }

        public Chat(string Mail1, string Mail2, string containt)
        {
            this.MailMembre1 = Mail1;
            this.MailMembre2 = Mail2;
            this.Containt = containt;
            this.MessageDate = DateTime.Now;
        }

        public Chat()
        {

        }

    }
}
