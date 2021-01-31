using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA_V_Be_A_Guide_Mod.Manager.SelectionManager
{
    public sealed class Selection_Manager
    {
        public static readonly Selection_Manager instance = new Selection_Manager();

        public static Selection_Manager Instance
        {
            get
            {
                return instance;
            }
        }

        public GTA.Math.Vector3 Destination;


        public enum Current_Selection
        {
            None = 0,
            Job_Selected = 1,
            Shop_Selected = 2
        }

        public Current_Selection current_Selection = Current_Selection.None;

        public enum Job_Selection
        {
            Hiker_Guide = 1,
            Mount_Bicycle_Guide = 2,
            Mount_Dirt_Bike_Guide = 3,
            Mount_ATV_Bike_Guide = 4,
            Sea_Diving_Instructor = 5,
            Beach_ATV_Bike_Guide = 6,
            Jet_Ski_Instructor = 7,
            Parachute_Instructor = 8
        }

        public Job_Selection job_Selection_enum = Job_Selection.Hiker_Guide;

        public List<dynamic> Job_Type_Selection_List = new List<dynamic>()
        {
            "Hiker Guide",
            "Mount Bicycle Guide",
            "Mount Dirt Bike Guide",
            "Mount ATV Bike Guide",
            "Sea Diving Instructor",
            "Beach ATV Bike Guide",
            "Jet Ski Instructor",
            "Parachute Instructor"
        };

        public string SelectedJob { get; set; }
    }
}
