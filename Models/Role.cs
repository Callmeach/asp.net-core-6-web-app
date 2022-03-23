namespace ProjectFirstSteps.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        public string? Name { get; set; } 

        public ICollection<Membre>? Membres { get; set;}
    }
}
