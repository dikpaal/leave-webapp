// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;
// using leave_webapp.Data;
// using leave_webapp.Models;

// namespace leave_webapp.Pages.LeaveApplications
// {
//     public class CreateModel : PageModel
//     {
//         private readonly ApplicationDbContext _context;

//         public CreateModel(ApplicationDbContext context)
//         {
//             _context = context;
//         }

//         [BindProperty]
//         public LeaveApplication LeaveApplication { get; set; }

//         public IActionResult OnGet()
//         {
//             return Page();
//         }

//         public async Task<IActionResult> OnPostAsync()
//         {
//             if (!ModelState.IsValid)
//             {
//                 return Page();
//             }

//             _context.LeaveApplications.Add(LeaveApplication);
//             await _context.SaveChangesAsync();

//             return RedirectToPage("./Index");
//         }
//     }
// }

using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using leave_webapp.Data;
using leave_webapp.Models;

namespace leave_webapp.Pages.LeaveApplications
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public CreateModel(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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

            await SendEmailAsync(LeaveApplication);

            return RedirectToPage("./Index");
        }

        private async Task SendEmailAsync(LeaveApplication leaveApplication)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            var smtpServer = emailSettings["SmtpServer"];
            var smtpPort = int.Parse(emailSettings["SmtpPort"]);
            var smtpUser = emailSettings["SmtpUser"];
            var smtpPass = emailSettings["SmtpPass"];
            var fromEmail = emailSettings["FromEmail"];
            var toEmail = emailSettings["ToEmail"];

            var message = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = "New Leave Application",
                Body = new StringBuilder()
                    .AppendLine("A new leave application has been submitted:")
                    .AppendLine($"EmployeeId: {leaveApplication.EmployeeId}")
                    .AppendLine($"FirstName: {leaveApplication.FirstName}")
                    .AppendLine($"LastName: {leaveApplication.LastName}")
                    .AppendLine($"Email: {leaveApplication.Email}")
                    .AppendLine($"Message: {leaveApplication.Message}")
                    .AppendLine($"Date: {leaveApplication.Date}")
                    .ToString(),
                IsBodyHtml = false,
            };

            message.To.Add(toEmail);

            using (var client = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true,
            })
            {
                await client.SendMailAsync(message);
            }
        }
    }
}
