using GTA_V_Be_A_Guide_Mod.Models.CheckPoint_Core_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace GTA_V_Be_A_Guide_Mod.Manager.CheckpointManager
{
    /// <summary>
    /// Class is responsible to import the checkpoint data from json files and then running the loop over it, depending on the
    /// job type
    /// </summary>
    public sealed class CheckpointManager
    {
        public static CheckpointManager instance = new CheckpointManager();
        public string checkpoint_Location_Name { get; set; }
        
        public GTA.Checkpoint []Checkpoint;
        
        public Dictionary<GTA.Math.Vector3, GTA.Checkpoint> created_Checkpoints_Dictionary = new Dictionary<GTA.Math.Vector3, GTA.Checkpoint>();
        public Dictionary<int, GTA.Checkpoint> completed_CheckPoints_Dictionary = new Dictionary<int, GTA.Checkpoint>();


        public static CheckpointManager Instance
        {
            get { return instance; }
        }

        public List<GTA.Math.Vector3> checkPoint_Final_List = new List<GTA.Math.Vector3>();


        public void Collect_CheckPoints(List<CheckPoint_Core_Model> checkPoint_List)
        {
            if(checkPoint_List != null)
            {   
                if ( checkPoint_List.Count > 0 )
                {                    
                    foreach (var item in checkPoint_List)
                    {   
                        if(item.Checkpoint_Mission_Location == checkpoint_Location_Name)
                        {
                            checkPoint_Final_List.Add(new GTA.Math.Vector3(item.Position_X, item.Position_Y, item.Position_Z));                            
                        }
                    }
                }
            }
        }
        
        public void Create_CheckPoint(List<GTA.Math.Vector3> checkpoint_List)
        {
            created_Checkpoints_Dictionary.Clear();
            
            if (checkpoint_List != null && checkpoint_List.Count > 0)
            {
                GTA.UI.Notification.Show(checkpoint_List.Count.ToString());
                int counter = 0;
                GTA.Checkpoint temp = null;
                foreach (var item in checkpoint_List)
                {
                    if(counter > checkpoint_List.Count - 2)
                    {
                        temp = GTA.World.CreateCheckpoint(GTA.CheckpointIcon.RingCheckerboard, item, checkpoint_List[checkpoint_List.IndexOf(item)], 2f, System.Drawing.Color.Green);
                    }
                    else
                    {
                        temp = GTA.World.CreateCheckpoint(GTA.CheckpointIcon.CylinderCycleArrow, item, checkpoint_List[checkpoint_List.IndexOf(item) + 1], 2f, System.Drawing.Color.Yellow);
                    }                    
                    
                    created_Checkpoints_Dictionary.Add(item, temp);
                    
                    counter++;                    
                }
            }           
        }

        
        public void Check_Current_CheckPointStatus(Dictionary<GTA.Math.Vector3, GTA.Checkpoint> checkpoint_Dictionary_Param)
        {

            foreach (var item in checkpoint_Dictionary_Param)
            {
                if (item.Value != null)
                {
                    if (GTA.World.GetDistance(item.Key, GTA.Game.Player.Character.Position) <= 5f)
                    {
                        item.Value.Delete();                        

                        if (!completed_CheckPoints_Dictionary.ContainsKey(checkpoint_Dictionary_Param.ToList().IndexOf(item)))
                        {
                            completed_CheckPoints_Dictionary.Add(checkpoint_Dictionary_Param.ToList().IndexOf(item), item.Value);
                            GTA.Audio.ReleaseSound(GTA.Audio.PlaySoundFrontend("SELECT", "HUD_FRONTEND_DEFAULT_SOUNDSET"));
                        }
                    }
                }
            }
        }

        public void Clear_All_Checkpoint_Lists_Dictionaries()
        {
            if (created_Checkpoints_Dictionary.Count > 0)
            {
                foreach (var item in created_Checkpoints_Dictionary)
                {
                    item.Value.Delete();
                }
            }
            created_Checkpoints_Dictionary.Clear();
            checkPoint_Final_List.Clear();
            completed_CheckPoints_Dictionary.Clear();            
        }
    }
}
