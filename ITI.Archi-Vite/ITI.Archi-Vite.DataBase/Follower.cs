using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.DataBase
{
    public class Follower
    {
        [Required]
        public string FilePath { get; set; }
        [Key]
        [Column(Order = 1)]
        public int ProfessionnalId { get; set; }
        [ForeignKey("ProfessionnalId")]
        public Professional Professionnal { get; set; }
        [Key]
        [Column(Order = 0)]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}
