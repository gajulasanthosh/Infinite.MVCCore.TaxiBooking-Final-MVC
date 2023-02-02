using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.MVCCore.TaxiBooking.Models
{
    public class EmployeeRosterViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide From Date")]
        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "Please provide ToDate")]
        [Display(Name = "ToDate")]
        public DateTime ToDate { get; set; }

        [Required(ErrorMessage = "Please provide In Time")]
        [Display(Name = "In Time")]
        public DateTime InTime { get; set; }

        [Required(ErrorMessage = "Please provide Out Time")]
        [Display(Name = "Out Time")]
        public DateTime OutTime { get; set; }
        public int EmployeeId { get; set; }
    }
}
