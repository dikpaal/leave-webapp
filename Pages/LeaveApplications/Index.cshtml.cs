using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using leave_webapp.Data;
using leave_webapp.Models;

namespace leave_webapp.Pages.LeaveApplications
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<LeaveApplication> LeaveApplication { get; set; }

        public async Task OnGetAsync()
        {
            LeaveApplication = await _context.LeaveApplications.ToListAsync();
        }
    }
}
