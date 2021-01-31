using GTA.Math;
using GTA_V_Be_A_Guide_Mod.Data.Export;
using GTA_V_Be_A_Guide_Mod.Manager.SelectionManager;
using GTA_V_Be_A_Guide_Mod.Models.Entities_Core_Model;
using GTA_V_Be_A_Guide_Mod.Models.JobType_Core_Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA_V_Be_A_Guide_Mod.Data.Import
{
    public sealed class Import_Details
    {
        public static readonly Import_Details instance = new Import_Details();

        public static Import_Details Instance
        {
            get
            {
                return instance;
            }
        }


        public List<Vector3> Locations_List = new List<Vector3>();
        public List<Vector3> Shop_List = new List<Vector3>();

        public Dictionary<int, Vector3> Location_List_Dictionary = new Dictionary<int, Vector3>();
        public Dictionary<int, Vector3> Shop_List_Dictionary = new Dictionary<int, Vector3>();

        internal bool _import_Purchase_List = true;
        public List<Purchasable_Items> Purchsable_Items_List_Import = new List<Purchasable_Items>();
        public List<Purchasable_Items> Purchsable_Items_List_Final = new List<Purchasable_Items>();

        public Import_Details()
        {

        }

        public void Import_Locations(bool mission_location_Path, bool store_location_Path)
        {
            string file_Path = "";
            if (mission_location_Path)
            {
                file_Path = @"..\Grand Theft Auto V\Scripts\Be A Guide\Locations\Locations.json";
            }else if (store_location_Path)
            {
                file_Path = @"..\Grand Theft Auto V\Scripts\Be A Guide\Locations\Store-Locations.json";
            }
            //var path = @"..\Grand Theft Auto V\Scripts\Be A Guide\Locations\Locations.json";

            var info = new FileInfo(file_Path);
            if ((!info.Exists) || info.Length == 0)
            {
                return;
            }

            if (mission_location_Path)
            {
                var temp_List = Import(file_Path, Export_Details.instance.Job_Locations);
                Location_List_Dictionary.Clear();
                
                for (int i = 0; i < temp_List.Count; i++)
                {
                    if(temp_List[i].Mission_Type == Selection_Manager.instance.job_Selection_enum.ToString())
                    {
                        Location_List_Dictionary.Add(i, new Vector3(temp_List[i].Position_X, temp_List[i].Position_Y, temp_List[i].Position_Z));
                    }
                    
                }
                
                mission_location_Path = false;
            }
            

            if (store_location_Path)
            {
                Import(file_Path, Export_Details.instance.Shop_Locations);

                var temp_List = Import(file_Path, Export_Details.instance.Shop_Locations);
                Shop_List_Dictionary.Clear();
                for (int i = 0; i < temp_List.Count; i++)
                {
                    //if(temp_List[i].Mission_Type == Selection_Manager.instance.job_Selection_enum.ToString())
                    {
                        Shop_List_Dictionary.Add(i, new Vector3(temp_List[i].Position_X, temp_List[i].Position_Y, temp_List[i].Position_Z));
                    }                    
                }
                
                Purchsable_Items_List_Final.Clear();
                file_Path = @"..\Grand Theft Auto V\Scripts\Be A Guide\Purchasable Items\Purchasable_Items.json";

                if (_import_Purchase_List)
                {
                    Import(file_Path, Purchsable_Items_List_Import);
                    _import_Purchase_List = false;
                }

                foreach (var item in Purchsable_Items_List_Import)
                {
                    if (item.Mission_Type == "Common" || item.Mission_Type == Selection_Manager.instance.job_Selection_enum.ToString())
                    {
                        //item.Item_Price = decimal.Parse(item.Item_Price);
                        Purchsable_Items_List_Final.Add(item);
                    }
                }
                //GTA.UI.Notification.Show(Selection_Manager.instance.job_Selection_enum.ToString());

                //foreach (var item in Purchsable_Items_List_Import)
                //{
                //    //if (item.Mission_Type != "Common" || item.Mission_Type != Selection_Manager.instance.job_Selection_enum.ToString())
                //    //{
                //    //    Purchsable_Items_List.Remove(item);
                //    //}
                //    //GTA.UI.Notification.Show(item.Item_Name);

                //}
                store_location_Path = false;
            }
        }


        public List<T> Import<T>(string path, List<T> location_list)
        {
            location_list.Clear();

            var lines = File.ReadAllLines(path);
            foreach (var item in lines)
            {
                location_list.Add(JsonConvert.DeserializeObject<T>(item));
            }

            return location_list;
        }
    }
}
