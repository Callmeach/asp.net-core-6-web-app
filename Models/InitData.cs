using Microsoft.EntityFrameworkCore;

namespace ProjectFirstSteps.Models
{
    public class InitData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MyContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MyContext>>()))
            {
                if (context.Roles.Any())
                {
                    return;
                }
                var roles = new List<Role>
                {
                    new Role
                    {
                        Name = "Administrateur"
                    } ,
                    new Role
                    {
                        Name = "Moderateur"
                    } ,
                    new Role
                    {
                        Name = "Utilisateur"
                    } ,

                };
                context.Roles.AddRange(roles);
                context.SaveChanges();
            }
        }
    }
}
