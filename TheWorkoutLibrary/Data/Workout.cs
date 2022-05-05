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
        public int workoutID { get; set; }
        [StringLength(25)]
        public string workoutName { get; set; }
        [StringLength(100)]
        public string workoutNotes { get; set; }
        [StringLength(25)]
        public string workoutDifficulty { get; set; }
        public Boolean activityStatus { get; set; }
        public byte[] videoData { get; set; }
    }
}
