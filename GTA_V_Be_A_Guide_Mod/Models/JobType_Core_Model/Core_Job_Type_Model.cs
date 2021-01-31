using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA_V_Be_A_Guide_Mod.Models.JobType_Core_Model
{
    public class Core_Job_Type_Model
    {   
        public int Location { get; set; }
        public float Position_X { get; set; }
        public float Position_Y { get; set; }
        public float Position_Z { get; set; }
        public string Mission_Type { get; set; }
        public string Difficulty_Type { get; set; }
    }
}
