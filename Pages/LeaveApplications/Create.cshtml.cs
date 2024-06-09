using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using leave_webapp.Data;
using leave_webapp.Models;

namespace leave_webapp.Pages.LeaveApplications
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LeaveApplication LeaveApplication { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.LeaveApplications.Add(LeaveApplication);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
