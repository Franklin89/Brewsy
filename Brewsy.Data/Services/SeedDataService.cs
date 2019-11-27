using Brewsy.Domain.Entities;
using Brewsy.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Brewsy.Data.Services
{
    public class SeedDataService : ISeedDataService
    {
        private readonly BrewsyContext _brewsyContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedDataService(BrewsyContext brewsyContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _brewsyContext = brewsyContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task EnsureSeedDataExists()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!_userManager.Users.Any())
            {
                var user = new User()
                {
                    UserName = "brewsy@mailinator.com",
                    Email = "brewsy@mailinator.com"
                };
                await _userManager.CreateAsync(user, "P@ssword1");
                await _userManager.AddToRoleAsync(user, "Admin");


                // Add my beers
                _brewsyContext.Beers.Add(new Beer 
                {
                    Name = "Simmentaler Mountain Pale Ale",
                    Description = "Sweet grain and bread malts, caramel, molasses, sweet fruits. Hazy, dark gold, small, creamy, off-white head. Light sweet. More creamy, sweet grain and bread malts, light bitterness coming up, straw, soft to medium carbonation, medium bodied. Decent, a touch sweet.",
                    Price = 5.5m,
                    UserId = user.Id,
                    ImageUrl = "https://shop.bierliebe.ch/wp-content/uploads/2018/07/Bierliebe-Webshop_Simmentaler-Braumanufaktur_Mountain-Pale-Ale.png"
                });

                _brewsyContext.Beers.Add(new Beer
                {
                    Name = "Surly Xtra-Citra Pale Ale",
                    Description = "This Americanized English-style Bitter is light gold in color, has a touch of malt sweetness and a ton of Citra hop aroma.",
                    Price = 4.25m,
                    UserId = user.Id,
                    ImageUrl = "https://res.cloudinary.com/ratebeer/image/upload/e_trim:1/d_beer_img_default.png,f_auto/beer_398459"
                });

                _brewsyContext.Beers.Add(new Beer
                {
                    Name = "Gigantic / Three Floyds Axes of Evil",
                    Description = "Beer #3 in the endless series of limited edition artist and artisan beers. Gigantic and Three Floyds unleash this true weapon of mass distraction. Citrus and tropical hoppyness lead to fill malt flavour from proper English malt, kilned over Welsh coal. Brewed for those of us that live and die in the Timbers Army and Section 8.",
                    Price = 6.5m,
                    UserId = user.Id,
                    ImageUrl = "https://res.cloudinary.com/ratebeer/image/upload/e_trim:1/d_beer_img_default.png,f_auto/beer_178679"
                });

                _brewsyContext.Beers.Add(new Beer
                {
                    Name = "Oasis Texas London Homesick",
                    Description = "A bright Texas session ale made with copious amounts of English Challenger hops and a classic English yeast strain –this beer is sure to make you feel right at home, no matter where you find yourself.",
                    Price = 4.0m,
                    UserId = user.Id,
                    ImageUrl = "https://res.cloudinary.com/ratebeer/image/upload/e_trim:1/d_beer_img_default.png,f_auto/beer_266510"
                });

                _brewsyContext.SaveChanges();
            }
        }
    }
}
