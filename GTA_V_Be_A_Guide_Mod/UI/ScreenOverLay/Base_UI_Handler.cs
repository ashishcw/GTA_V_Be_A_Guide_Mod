using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms.VisualStyles;

namespace GTA_V_Be_A_Guide_Mod.UI.ScreenOverLay
{
    public sealed class Base_UI_Handler
    {
        public readonly static Base_UI_Handler instance = new Base_UI_Handler() { };

        public static Base_UI_Handler Instance
        {
            get
            {
                return instance;
            }
        }

        public enum Icon_Type_Final_enum
        {
            Group_Health_Status = 0,
            Group_Satisfaction_Status = 1,
            Group_Energy_Status = 2,
            Group_Rating_Status = 3,
        }

        public Icon_Type_Final_enum icon_Type_Final_Enum = Icon_Type_Final_enum.Group_Energy_Status;

        Dictionary<string, Icon_Type_Final_enum> Custom_Sprite_Collection_Dictionary = new Dictionary<string, Icon_Type_Final_enum>();

        public Dictionary<GTA.UI.CustomSprite, Icon_Type_Final_enum> Custom_Sprite_Collection_MedKit = new Dictionary<GTA.UI.CustomSprite, Icon_Type_Final_enum>();
        public Dictionary<GTA.UI.CustomSprite, Icon_Type_Final_enum> Custom_Sprite_Collection_Hearts_Satisfaction = new Dictionary<GTA.UI.CustomSprite, Icon_Type_Final_enum>();
        public Dictionary<GTA.UI.CustomSprite, Icon_Type_Final_enum> Custom_Sprite_Collection_Rating = new Dictionary<GTA.UI.CustomSprite, Icon_Type_Final_enum>();
        public Dictionary<GTA.UI.CustomSprite, Icon_Type_Final_enum> Custom_Sprite_Collection_Energy = new Dictionary<GTA.UI.CustomSprite, Icon_Type_Final_enum>(); 

        internal float menu_Y_Position = 500f, target_Y_Position = 40f;




        public Base_UI_Handler()
        {
            Initialize();
        }

        private void Initialize()
        {
            //Satisfaction Heart Sprites Load
            icon_Type_Final_Enum = Icon_Type_Final_enum.Group_Satisfaction_Status; 
            string path = @"..\Grand Theft Auto V\Scripts\Be A Guide\UI\Base Sprites\GroupSatisfaction\";            
            Load_Sprites(path, icon_Type_Final_Enum, Custom_Sprite_Collection_Dictionary);
            Custom_Sprite_Collection_Hearts_Satisfaction = CustomSprite_Object_Creation(Custom_Sprite_Collection_Dictionary, menu_Y_Position + (target_Y_Position * 1));

            //Health Kit Sprites Load
            icon_Type_Final_Enum = Icon_Type_Final_enum.Group_Health_Status;
            path = @"..\Grand Theft Auto V\Scripts\Be A Guide\UI\Base Sprites\GroupHealth\";
            Load_Sprites(path, icon_Type_Final_Enum, Custom_Sprite_Collection_Dictionary);
            Custom_Sprite_Collection_MedKit = CustomSprite_Object_Creation(Custom_Sprite_Collection_Dictionary, menu_Y_Position + (target_Y_Position * 2));

            //Energy Status
            icon_Type_Final_Enum = Icon_Type_Final_enum.Group_Energy_Status;
            path = @"..\Grand Theft Auto V\Scripts\Be A Guide\UI\Base Sprites\GroupEnergy\";
            Load_Sprites(path, icon_Type_Final_Enum, Custom_Sprite_Collection_Dictionary);
            Custom_Sprite_Collection_Energy = CustomSprite_Object_Creation(Custom_Sprite_Collection_Dictionary, menu_Y_Position + (target_Y_Position * 3));

            //Rating Sprites Load
            icon_Type_Final_Enum = Icon_Type_Final_enum.Group_Rating_Status;
            path = @"..\Grand Theft Auto V\Scripts\Be A Guide\UI\Base Sprites\GroupRating\";
            Load_Sprites(path, icon_Type_Final_Enum, Custom_Sprite_Collection_Dictionary);
            Custom_Sprite_Collection_Rating = CustomSprite_Object_Creation(Custom_Sprite_Collection_Dictionary, menu_Y_Position + (target_Y_Position * 4));

            
        }


        private void Load_Sprites(string path, Icon_Type_Final_enum Sprite_Category, Dictionary<string, Icon_Type_Final_enum> dictionary_Type)
        {
            dictionary_Type.Clear();
            if (Directory.Exists(path))
            {
                var all_Files = Directory.GetFiles(path);

                foreach (var item in all_Files)
                {
                    if (File.Exists(item))
                    {
                        dictionary_Type.Add(item, Sprite_Category);
                    }
                }
            }
        }


        private Dictionary<GTA.UI.CustomSprite, Icon_Type_Final_enum> CustomSprite_Object_Creation(Dictionary<string, Icon_Type_Final_enum> dictionary, float y)
        {
            var InputDictionary = new Dictionary<GTA.UI.CustomSprite, Icon_Type_Final_enum>();
            foreach (var item in dictionary)
            {
                if(item.Value == icon_Type_Final_Enum)
                {
                    var new_obj = new GTA.UI.CustomSprite(item.Key, new SizeF(180, 30), new PointF(1100f, y));
                    InputDictionary.Add(new_obj, icon_Type_Final_Enum);
                }
            }
            return InputDictionary;
        }


        public GTA.UI.CustomSprite Update_Sprite(bool addition, bool substraction, Dictionary<GTA.UI.CustomSprite, Icon_Type_Final_enum> sprite_Collection_Dictionary, GTA.UI.CustomSprite current_Sprite)
        {
            int Max_Count = sprite_Collection_Dictionary.Count;
            int current_Index = Max_Count - 1;
            if (addition)
            {
                current_Index = sprite_Collection_Dictionary.Keys.ToList().IndexOf(current_Sprite);
                //temp = Base_UI_Handler.instance.Custom_Sprite_Collection_All.ElementAtOrDefault(tempCount).Key;
                if (current_Index < Max_Count - 1)
                {
                    current_Index++;
                }                
            }else if (substraction)
            {
                current_Index = sprite_Collection_Dictionary.Keys.ToList().IndexOf(current_Sprite);
                //temp = Base_UI_Handler.instance.Custom_Sprite_Collection_All.ElementAtOrDefault(tempCount).Key;
                if (current_Index > 1)
                {
                    current_Index--;
                }                
            }
            
            current_Sprite = sprite_Collection_Dictionary.ElementAtOrDefault(current_Index).Key;

            return current_Sprite;
        }

    }
}
