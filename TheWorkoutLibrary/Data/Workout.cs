using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TheWorkoutLibrary.Data
{
    public class Workout
    {
        [Key]
        public int Id { get; set; }
        [StringLength(25)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Notes { get; set; }
        [StringLength(25)]
        public string Difficulty { get; set; }
        public int UserId { get; set; }
        [StringLength(125)]
        public string ImageURL { get; set; }
    }
}
