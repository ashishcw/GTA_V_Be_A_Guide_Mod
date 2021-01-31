using GTA;
using GTA.Math;
using GTA_V_Be_A_Guide_Mod.Data.Export;
using GTA_V_Be_A_Guide_Mod.Data.Import;
using GTA_V_Be_A_Guide_Mod.Manager.SelectionManager;
using GTA_V_Be_A_Guide_Mod.Manager.World;
using GTA_V_Be_A_Guide_Mod.Models.JobType_Core_Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA_V_Be_A_Guide_Mod.SimpleUIMenu.Purchase_Menu
{
    public sealed class Purchase_Menu
    {
        //Singletone
        public static readonly Purchase_Menu instance = new Purchase_Menu();

        public static Purchase_Menu Instance
        {
            get
            {
                return instance;
            }
        }

        public MenuPool purchase_MenuPool;
        public UIMenu purchase_Main_Menu;
        public UIMenu subMenu;        

        UIMenuNumberValueItem item_Quantity;
        
        UIMenuSubsectionItem subsectionItem2;
        

        public bool Mod_Activated = false;
        int qty;
        

        public Purchase_Menu()
        {
            //Setup menu
            //InitMenu();
        }


        public void InitMenu()
        {
            // First initialize an instance of a MenuPool.
            // A MenuPool object will manage all the interconnected
            // menus that you add to it.
            purchase_MenuPool = new MenuPool();

            // Initialize a menu, with name "Main Menu"
            purchase_Main_Menu = new UIMenu("Be a Tour Guide");
            // Add mainMenu to _menuPool
            purchase_MenuPool.AddMenu(purchase_Main_Menu);

            // Let's set the colors of the menu before adding other menus
            // so that submenus will also have the same color scheme.
            // Requires a reference to System.Drawing
            purchase_Main_Menu.TitleColor = Color.FromArgb(255, 90, 210, 237);
            purchase_Main_Menu.TitleBackgroundColor = Color.FromArgb(240, 0, 0, 0);
            purchase_Main_Menu.TitleUnderlineColor = Color.FromArgb(255, 237, 90, 90);
            purchase_Main_Menu.DefaultBoxColor = Color.FromArgb(160, 0, 0, 0);
            purchase_Main_Menu.DefaultTextColor = Color.FromArgb(230, 255, 255, 255);
            purchase_Main_Menu.HighlightedBoxColor = Color.FromArgb(130, 110, 204, 134);
            purchase_Main_Menu.HighlightedItemTextColor = Color.FromArgb(255, 255, 255, 255);
            purchase_Main_Menu.DescriptionBoxColor = Color.FromArgb(255, 0, 0, 0);
            purchase_Main_Menu.DescriptionTextColor = Color.FromArgb(255, 255, 255, 255);
            purchase_Main_Menu.SubsectionDefaultBoxColor = Color.FromArgb(160, 0, 0, 0);
            purchase_Main_Menu.SubsectionDefaultTextColor = Color.FromArgb(180, 255, 255, 255);


            subsectionItem2 = new UIMenuSubsectionItem("--- Purchasable Items ---");
            purchase_Main_Menu.AddMenuItem(subsectionItem2);

            //for (int i = 0; i < Import_Details.instance.Purchsable_Items_List_Final.Count; i++)
            //{
            //    //string menu_Item = "menu_Item_" + i.ToString();
            //    //UIMenuItem Purchasable_Item_[] = new UIMenuItem("Cola");
            //    purchase_Main_Menu.AddMenuItem(new UIMenuItem(Import_Details.instance.Purchsable_Items_List_Final[i].Item_Name + " : $" + Import_Details.instance.Purchsable_Items_List_Final[i].Item_Price));
            //}

            foreach (var item in Import_Details.instance.Purchsable_Items_List_Final)
            {
                purchase_Main_Menu.AddMenuItem(new UIMenuItem(item.Item_Name + " : $" + item.Item_Price));
            }

            purchase_Main_Menu.OnItemSelect += (sender, selectedItem, index) =>
            {
                //if (selectedItem == Purchasable_Item)
                //{

                //}
            };

            purchase_Main_Menu.OnItemLeftRight += MainMenu_OnItemLeftRight;
        }

        private void MainMenu_OnItemLeftRight(UIMenu sender, UIMenuItem selectedItem, int index, UIMenu.Direction direction)
        {
            // Check which item is selected.
            if (selectedItem == item_Quantity)
            {
                // ControlIntValue is an easy way to let a menu item control
                // a specific int with one line of code.
                // In this example, we will control the var "testInt" with
                // the "itemIntegerControl" menu item.
                // The params that follow are explained with intellisense.
                purchase_Main_Menu.ControlIntValue(ref qty, item_Quantity, direction, 1, 5, true, 0, 20);

                GTA.UI.Screen.ShowSubtitle("You pressed " + (direction == UIMenu.Direction.Left ? "Left" : "Right") + " while highlighting Integer Item!");
            }
            
        }
    }
}
