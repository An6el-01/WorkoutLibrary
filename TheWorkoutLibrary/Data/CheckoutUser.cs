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
        public string name { get; set; }

        public int basketID { get; set; }
    }
}
