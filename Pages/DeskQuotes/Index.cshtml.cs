using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDeskRazor.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MegaDeskRazor.Pages.DeskQuotes
{
    public class IndexModel : PageModel
    {
        private readonly MegaDeskRazor.Models.MegaDeskRazorContext _context;

        public IndexModel(MegaDeskRazor.Models.MegaDeskRazorContext context)
        {
            _context = context;
        }



        public IList<DeskQuote> DeskQuote { get; set; }
        [BindProperty(SupportsGet = true)]
        public IList<DeskQuote> DeskQuote2 { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        public SelectList Genres { get; set; }
        [BindProperty(SupportsGet = true)]
        public string MovieGenre { get; set; }
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public async Task OnGetAsync(string sortOrder)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<DeskQuote> deskIQ = from s in _context.DeskQuote
                                             select s;
            switch (sortOrder)
            {
                case "name_desc":
                    deskIQ = deskIQ.OrderByDescending(s => s.CustomerName);
                    break;
                case "Date":
                    deskIQ = deskIQ.OrderBy(s => s.QuoteDate);
                    break;
                case "date_desc":
                    deskIQ = deskIQ.OrderByDescending(s => s.QuoteDate);
                    break;
                default:
                    deskIQ = deskIQ.OrderBy(s => s.CustomerName);
                    break;
            }
                
            DeskQuote = await deskIQ.AsNoTracking().ToListAsync();
        }

    }
}
