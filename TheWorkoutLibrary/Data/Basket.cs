using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TheWorkoutLibrary.Data
{
    public class Basket
    {
        [Key]
        public int basketID { get; set; }
    }
}
