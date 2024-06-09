using System;
using System.ComponentModel.DataAnnotations;

namespace leave_webapp.Models
{
    public class LeaveApplication
    {
        [Key]
        public int EmployeeId { get; set; }

        [StringLength(20)]
        public string FirstName { get; set; }

        [StringLength(20)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Message { get; set; }

        public DateTime Date { get; set; }
    }
}
