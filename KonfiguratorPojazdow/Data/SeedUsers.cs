using Microsoft.AspNetCore.Identity;

namespace KonfiguratorPojazdow.Data
{
    public class SeedUsers
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            // Pobranie wymaganego usługi UserManager<IdentityUser> z dostawcy usług
            UserManager<IdentityUser> _userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Inicjalizacja listy SeedUserModel z danymi użytkowników
            var userlist = new List<SeedUserModel>()
            {
                new SeedUserModel(){ UserName="admin@admin.com", Password= "admiN2!" },
            };

            // Iteracja przez listę użytkowników
            foreach (var user in userlist)
            {
                // Sprawdzenie, czy użytkownik o podanym UserName już istnieje
                if (!_userManager.Users.Any(r => r.UserName == user.UserName))
                {
                    // Tworzenie nowego użytkownika IdentityUser
                    var newuser = new IdentityUser { UserName = user.UserName, Email = user.UserName };

                    // Tworzenie użytkownika w systemie Identity
                    var result = await _userManager.CreateAsync(newuser, user.Password);

                    // Dodawanie roli "Administrator" dla nowo utworzonego użytkownika
                    var roleResult = await _userManager.AddToRoleAsync(newuser, "Administrator");
                }
            }

            // Pobranie użytkownika o nazwie "admin@gmail.com"
            var usAdm = _userManager.Users.FirstOrDefault(f => f.UserName == "admin@admin.com");

            // Sprawdzenie, czy użytkownik jest w roli "Administrator"
            var isAdm = await _userManager.IsInRoleAsync(usAdm, "Administrator");

            // Pobranie menedżera ról z dostawcy usług
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            // Sprawdzenie, czy rola "Administrator" już istnieje
            bool b = await roleManager.RoleExistsAsync("Administrator");

            // Jeśli użytkownik nie jest w roli "Administrator"
            if (!isAdm)
            {
                // Dodanie użytkownika do roli "Administrator"
                var addAdm = await _userManager.AddToRoleAsync(usAdm, "Administrator");
            }
        }
    }
}
