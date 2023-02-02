using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.MVCCore.TaxiBooking.Models
{
    public class BookingViewModel
    {
        public int BookingId { get; set; }
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Please Provide Booking Date")]
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }

        [Required(ErrorMessage = "Please Provide Trip Date")]
        [Display(Name = "Trip Date")]
        public DateTime TripDate { get; set; }

        [Required(ErrorMessage = "Please Provide Start time")]
        [Display(Name = "start time")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Please Provide End time")]
        [Display(Name = "End time")]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Please Provide Source Address")]
        [Display(Name = "Source Address")]
        public string SourceAddress { get; set; }

        [Required(ErrorMessage = "Please Provide Destination Address")]
        [Display(Name = "Destination Address")]
        public string DestinationAddress { get; set; }

        [Required(ErrorMessage ="Please select Taxi Model")]
        [Display(Name ="Taxi Model")]
        public int TaxiId { get; set; }
        public string CustomerName { get; set; }

        public string Status { get; set; }

        public List<TaxiVM> Taxis { get; set; }
    }
}
