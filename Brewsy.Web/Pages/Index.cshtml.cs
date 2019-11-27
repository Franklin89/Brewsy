using Brewsy.Data;
using Brewsy.Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Brewsy.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BrewsyContext _brewsyContext;

        public IndexModel(BrewsyContext brewsyContext)
        {
            _brewsyContext = brewsyContext;
        }

        public List<Beer> Beers { get; set; }

        public void OnGet()
        {
            Beers = _brewsyContext.Beers.Include(x => x.User).Where(x => x.User != null).ToList();
        }
    }
}
