// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.Logging;
// using System;
// using System.Collections.Concurrent;
// using System.Net;
// using System.Net.Mail;
// using System.Text;
// using System.Threading;
// using System.Threading.Tasks;
// using leave_webapp.Models;

// namespace leave_webapp.Services
// {
//     public class EmailSenderService : BackgroundService
//     {
//         private readonly ConcurrentQueue<LeaveApplication> _emailQueue = new ConcurrentQueue<LeaveApplication>();
//         private readonly IConfiguration _configuration;
//         private readonly ILogger<EmailSenderService> _logger;

//         public EmailSenderService(IConfiguration configuration, ILogger<EmailSenderService> logger)
//         {
//             _configuration = configuration;
//             _logger = logger;
//         }

//         public void QueueEmail(LeaveApplication leaveApplication)
//         {
//             _emailQueue.Enqueue(leaveApplication);
//         }

//         protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//         {
//             while (!stoppingToken.IsCancellationRequested)
//             {
//                 if (_emailQueue.TryDequeue(out var leaveApplication))
//                 {
//                     try
//                     {
//                         await SendEmailAsync(leaveApplication);
//                     }
//                     catch (Exception ex)
//                     {
//                         _logger.LogError(ex, "Error sending email");
//                     }
//                 }

//                 await Task.Delay(1000, stoppingToken); // Wait for 1 second before checking the queue again
//             }
//         }

//         private async Task SendEmailAsync(LeaveApplication leaveApplication)
//         {
//             var emailSettings = _configuration.GetSection("EmailSettings");

//             var smtpServer = emailSettings["SmtpServer"];
//             var smtpPort = int.Parse(emailSettings["SmtpPort"]);
//             var smtpUser = emailSettings["SmtpUser"];
//             var smtpPass = emailSettings["SmtpPass"];
//             var fromEmail = emailSettings["FromEmail"];
//             var toEmail = emailSettings["ToEmail"];

//             var message = new MailMessage
//             {
//                 From = new MailAddress(fromEmail),
//                 Subject = "New Leave Application",
//                 Body = new StringBuilder()
//                     .AppendLine("A new leave application has been submitted:")
//                     .AppendLine($"EmployeeId: {leaveApplication.EmployeeId}")
//                     .AppendLine($"FirstName: {leaveApplication.FirstName}")
//                     .AppendLine($"LastName: {leaveApplication.LastName}")
//                     .AppendLine($"Email: {leaveApplication.Email}")
//                     .AppendLine($"Message: {leaveApplication.Message}")
//                     .AppendLine($"Date: {leaveApplication.Date}")
//                     .ToString(),
//                 IsBodyHtml = false,
//             };

//             message.To.Add(toEmail);

//             using (var client = new SmtpClient(smtpServer, smtpPort)
//             {
//                 Credentials = new NetworkCredential(smtpUser, smtpPass),
//                 EnableSsl = true,
//             })
//             {
//                 await client.SendMailAsync(message);
//             }
//         }
//     }
// }
