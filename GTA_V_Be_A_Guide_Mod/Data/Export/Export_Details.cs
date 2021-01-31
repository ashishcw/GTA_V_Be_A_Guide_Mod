using GTA_V_Be_A_Guide_Mod.Manager.SelectionManager;
using GTA_V_Be_A_Guide_Mod.Models.Entities_Core_Model;
using GTA_V_Be_A_Guide_Mod.Models.JobType_Core_Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA_V_Be_A_Guide_Mod.Data.Export
{   
    public sealed class Export_Details
    {
        public static readonly Export_Details instance = new Export_Details();

        public static Export_Details Instance
        {
            get
            {
                return instance;
            }
        }

        public List<Core_Job_Type_Model> Job_Locations = new List<Core_Job_Type_Model>();

        public List<Core_Entities_Model_Definition> Shop_Locations = new List<Core_Entities_Model_Definition>();

        public void Export<T>(ref T export_model, string path)
        {
            var temp = JsonConvert.SerializeObject(export_model);

            using (var writer = File.AppendText(path))//using (var writer = new StreamWriter(path))
            {
                writer.WriteLine(temp);

                writer.Flush();
                writer.Close();
            }
        }



    }
}
