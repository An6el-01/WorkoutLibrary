using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace TheWorkoutLibrary.Data
{
    public class CheckoutUser
    {
        [Key, StringLength(50)]
        public string email { get; set; }
        [StringLength(50)]
        public string firstName { get; set; }
        [StringLength(50)]
        public string lastName { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public Boolean subscriptionStatus { get; set; }
        public byte[] imageData { get; set; }
        public int basketID { get; set; }
    }
}
