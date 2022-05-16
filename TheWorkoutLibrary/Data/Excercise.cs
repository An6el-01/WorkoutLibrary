using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorkoutLibrary.Data
{
    public class Excercise
    {
        [Key]
        public int Id { get; set; }
        [StringLength(25)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Notes { get; set; }
        [StringLength(25)]
        public string Difficulty { get; set; }
        public Boolean Visibility { get; set; }
        [StringLength(125)]
        public string YoutubeURL { get; set; }
    }
}
