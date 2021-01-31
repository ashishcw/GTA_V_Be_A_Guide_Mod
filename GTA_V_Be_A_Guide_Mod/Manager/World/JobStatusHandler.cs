using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA_V_Be_A_Guide_Mod.Manager.World
{
    public sealed class JobStatusHandler
    {
        //Singleton
        public static readonly JobStatusHandler instance = new JobStatusHandler();

        public static JobStatusHandler Instance
        {
            get
            {
                return instance;
            }
        }


        public enum Job_Current_State
        {
            InProgress = 0, //When the job is started
            Cancelled = 1, //When the job is canclled
            Active = 2, //When the user is in job selection menu/state
            None = 3 //Else than above
        }

        public Job_Current_State job_Current_State = Job_Current_State.None;

    }//Class ends here
}//Namespace ends here
