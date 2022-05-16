using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TheWorkoutLibrary.Data
{
    public class WorkoutExcercise
    {
        [Key]
        public int Id { get; set; }       
        public int WorkoutId { get; set; }
        public int ExcerciseId { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int UserId { get; set; }
    }
}
