using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Brewsy.Data;
using Brewsy.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Brewsy.Web
{
    public class CreateModel : PageModel
    {
        private readonly BrewsyContext _brewsyContext;

        [BindProperty]
        public Beer Beer { get; set; }

        public CreateModel(BrewsyContext brewsyContext)
        {
            _brewsyContext = brewsyContext;
        }

        public void OnGet()
        {
            Beer = new Beer();
        }

        public IActionResult OnPost()
        {
            Beer.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                _brewsyContext.Beers.Add(Beer);
                _brewsyContext.SaveChanges();
                return RedirectToPage("/beers/index");
            }

            return Page();
        }
    }
}