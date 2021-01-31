using GTA.Math;
using GTA_V_Be_A_Guide_Mod.Data.Import;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA_V_Be_A_Guide_Mod.Manager.World
{
    /// <summary>
    /// This class is responsible to keep the track of all the events in the world, related to the mod
    /// </summary>
    public sealed class World_Manager
    {
        public static readonly World_Manager instance = new World_Manager();

        public static World_Manager Instance
        {
            get
            {
                return instance;
            }
        }

        public World_Manager()
        {

        }

        public void World_Location_Marker_Display(bool routeActivation ,Vector3 MarkerPosition)
        {
            if (!routeActivation)
            {
                //Display the router if the player is close enough to the marker
                if(GTA.World.GetDistance(GTA.Game.Player.Character.Position, MarkerPosition) <= 25f)
                {
                    GTA.World.DrawMarker(GTA.MarkerType.UpsideDownCone, new Vector3(MarkerPosition.X, MarkerPosition.Y, MarkerPosition.Z + 0.5f), Vector3.Zero, Vector3.Zero, new Vector3(0.5f, 0.5f, 0.5f), Color.Yellow, true);

                    if (GTA.World.GetDistance(GTA.Game.Player.Character.Position, MarkerPosition) <= 5f)
                    {
                        GTA.UI.Screen.ShowHelpTextThisFrame("Press E to open Menu");
                    }
                }
            }
        }

        public void Activate_Waypoint(Vector3 Target_Position)
        {
            GTA.World.WaypointPosition = Target_Position;
        }



        public Vector3 Nearest_Location(Dictionary<int, Vector3> dictionary, GTA.Math.Vector3 Target_Position)
        {
            //Import_Details.instance.Locations_List.Clear();
            if(dictionary.Count == 0)
            {
                GTA.UI.Notification.Show("No nearest location found");
                return Vector3.Zero;
            }

            float lastreading = 9999f, currentreading = 0f;
            int final_key = 0;
            foreach (var item in dictionary)
            {
                currentreading = GTA.World.GetDistance(Target_Position, item.Value);

                if (currentreading < lastreading)
                {
                    lastreading = currentreading;
                    final_key = item.Key;
                }
                //Import_Details.instance.Locations_List.Add(item.Value);
            }
            GTA.UI.Notification.Show("Nearest Location is " + lastreading.ToString("0.##") + " meters away");

            return dictionary[final_key];
        }
    }
}
