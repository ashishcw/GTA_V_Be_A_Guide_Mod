using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA_V_Be_A_Guide_Mod.Models.Entities_Core_Model
{
    public class Purchasable_Items
    {
        //Item-ID
        public int Item_ID { get; set; }
        public string Item_Name { get; set; }
        public int Item_Price { get; set; }
        public string Mission_Type { get; set; }
    }
}
