using GTA_V_Be_A_Guide_Mod.Manager.World.Entity_Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA_V_Be_A_Guide_Mod.Manager.World
{
    public sealed class Ped_Handler : IEntity_Handler<>
    {
        public static Ped_Handler instance = new Ped_Handler();

        public static Ped_Handler Instance
        {
            get
            {
                return instance;
            }
        }
        public string Entity_Type { get { return "Ped"; } set { } }
        public int Max_Entities_Count { get { return 10; } set { } }


        public Ped_Handler()
        {

        }




    }//Class ends here
}//Namespace ends here
