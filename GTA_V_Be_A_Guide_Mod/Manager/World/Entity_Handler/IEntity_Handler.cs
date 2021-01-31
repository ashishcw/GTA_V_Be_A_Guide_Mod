using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA_V_Be_A_Guide_Mod.Manager.World.Entity_Handler
{
    interface IEntity_Handler<T>
    {
        string Entity_Type { get;  set; }

        int Max_Entities_Count { get; set; }

        List<T> Entities_List { get; set; }

    }
}
