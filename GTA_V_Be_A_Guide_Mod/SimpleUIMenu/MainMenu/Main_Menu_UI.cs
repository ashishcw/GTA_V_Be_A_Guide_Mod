using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GTA;
using GTA.Math;
using GTA_V_Be_A_Guide_Mod.Data.Export;
using GTA_V_Be_A_Guide_Mod.Data.Import;
using GTA_V_Be_A_Guide_Mod.Manager.CheckpointManager;
using GTA_V_Be_A_Guide_Mod.Manager.SelectionManager;
using GTA_V_Be_A_Guide_Mod.Manager.World;
using GTA_V_Be_A_Guide_Mod.Models.CheckPoint_Core_Model;
using GTA_V_Be_A_Guide_Mod.Models.JobType_Core_Model;

namespace GTA_V_Be_A_Guide_Mod.SimpleUIMenu.MainMenu
{
    public sealed class Main_Menu
    {
        //Singletone
        public static readonly Main_Menu instance = new Main_Menu();

        public static Main_Menu Instance
        {
            get
            {
                return instance;
            }
        }
        
        public MenuPool _menuPool;
        public UIMenu mainMenu;
        public UIMenu subMenu;
        
        UIMenuItem Active_Mod;
        
        UIMenuNumberValueItem itemIntegerControl;
        UIMenuNumberValueItem itemFloatControl;
        UIMenuNumberValueItem Job_Type;
        UIMenuSubsectionItem subsectionItem2;
        UIMenuListItem jobTypeList;
        UIMenuListItem itemListControlAdvanced;
        UIMenuItem itemAddPerson;
        UIMenuItem itemRemoveLastPerson;

        UIMenuItem submenuItem1;
        UIMenuItem submenuItem2;

        public bool Mod_Activated = false;
        int testInt;
        float testFloat;
        
        List<dynamic> testListString = new List<dynamic>()
        {
            "List Item 1",
            "List Item 2",
            "List Item 3",
            "List Item 4"
        };
        List<dynamic> testListAdvanced = new List<dynamic>()
        {
            //new Person("Michael", "Scott", 8008),
            //new Person("Dwight", "Schrute", 1337),
            //new Person("Stanley", "Hudson", 101)
        };


        public Main_Menu()
        {
            //Setup menu
            InitMenu();
        }


        public void InitMenu()
        {
            // First initialize an instance of a MenuPool.
            // A MenuPool object will manage all the interconnected
            // menus that you add to it.
            _menuPool = new MenuPool();

            // Initialize a menu, with name "Main Menu"
            mainMenu = new UIMenu("Be a Tour Guide");
            // Add mainMenu to _menuPool
            _menuPool.AddMenu(mainMenu);

            // Let's set the colors of the menu before adding other menus
            // so that submenus will also have the same color scheme.
            // Requires a reference to System.Drawing
            mainMenu.TitleColor = Color.FromArgb(255, 90, 210, 237);
            mainMenu.TitleBackgroundColor = Color.FromArgb(240, 0, 0, 0);
            mainMenu.TitleUnderlineColor = Color.FromArgb(255, 237, 90, 90);
            mainMenu.DefaultBoxColor = Color.FromArgb(160, 0, 0, 0);
            mainMenu.DefaultTextColor = Color.FromArgb(230, 255, 255, 255);
            mainMenu.HighlightedBoxColor = Color.FromArgb(130, 110, 204, 134);
            mainMenu.HighlightedItemTextColor = Color.FromArgb(255, 255, 255, 255);
            mainMenu.DescriptionBoxColor = Color.FromArgb(255, 0, 0, 0);
            mainMenu.DescriptionTextColor = Color.FromArgb(255, 255, 255, 255);
            mainMenu.SubsectionDefaultBoxColor = Color.FromArgb(160, 0, 0, 0);
            mainMenu.SubsectionDefaultTextColor = Color.FromArgb(180, 255, 255, 255);

            // A string attached to the end of submenu's menu item text
            // to indicate that the item leads to a submenu.
            _menuPool.SubmenuItemIndication = "  ~r~>";

            #region SUBMENU_SETUP

            // Initialize another menu, with name "Submenu"
            subMenu = new UIMenu("Submenu");
            // Add subMenu to _menuPool as a child menu of mainMenu.
            // This will create a menu item in mainMenu with the name "Go to Submenu",
            // and selecting it will bring you to the submenu.
            
            //_menuPool.AddSubMenu(subMenu, mainMenu, "Go to Submenu");

            // Initialize an item called "Submenu Item 1"
            // and add it to the submenu.
            submenuItem1 = new UIMenuItem("Submenu Item 1");
            //subMenu.AddMenuItem(submenuItem1);

            // Same as above
            submenuItem2 = new UIMenuItem("Submenu Item 2");
            //subMenu.AddMenuItem(submenuItem2);

            #endregion

            // A UIMenuSubsectionItem is essentially just a splitter.
            //subsectionItem = new UIMenuSubsectionItem("--- Splitter ---");

            // Add subsectionItem to the mainMenu.
            // It will appear after the subMenu item,
            // since this is the order we are affecting mainMenu.


            // Just adding some more items to mainMenu.
            // Second param is the default value.
            // Third param is a description that appears at the bottom
            // of the menu.
            // UIMenuNumberValueItem is just like UIMenuItem but with "<" and ">"
            // wrapped around the value.            

            Active_Mod  = new UIMenuItem("Activate Mod?", Mod_Activated, "Do you wish to activate the mod?");
            mainMenu.AddMenuItem(Active_Mod);

            itemIntegerControl = new UIMenuNumberValueItem("Integer Item", testInt, "This item controls an integer.");
            //mainMenu.AddMenuItem(itemIntegerControl);

            itemFloatControl = new UIMenuNumberValueItem("Float Item", testFloat, "This item controls a float.");
            //mainMenu.AddMenuItem(itemFloatControl);

            Job_Type = new UIMenuNumberValueItem("Job Type", Selection_Manager.instance.job_Selection_enum, "Select the job type do you wish to perform");
            //mainMenu.AddMenuItem(Job_Type);

            subsectionItem2 = new UIMenuSubsectionItem("--- List Stuff ---");
            //mainMenu.AddMenuItem(subsectionItem2);

            // the 3rd param must be of type List<dynamic>
            // or you will get a compile time error.
            jobTypeList = new UIMenuListItem("Job Type", "Select the job type do you wish to perform", Selection_Manager.instance.Job_Type_Selection_List);
            

            UIMenuItem Finalize_Job_Type = new UIMenuItem("Done");
            

            UIMenuItem Cancle_Job_Type = new UIMenuItem("Cancle Current Job");
            

            //itemListControlAdvanced = new UIMenuListItem("People", "A list of people", testListAdvanced);
            //mainMenu.AddMenuItem(itemListControlAdvanced);

            itemAddPerson = new UIMenuItem("Add a person");
            //mainMenu.AddMenuItem(itemAddPerson);

            itemRemoveLastPerson = new UIMenuItem("Remove last person");
            //mainMenu.AddMenuItem(itemRemoveLastPerson);

            // Now let's create some events.
            // All events are in the UIMenu class.
            // You can create a specific or anonymous method.

            // Let's subscribe mainMenu's OnItemSelect event to an anonymous method.
            // This method will be executed whenever you press Enter, Numpad5, or
            // the Select button on a gamepad while mainMenu is open.            
            UIMenuItem Get_CoOrdinates = new UIMenuItem("Get Current Coordinate");
            UIMenuItem Create_New_CheckPoint = new UIMenuItem("Create New Checkpoint");
            UIMenuItem Nearest_Job_Location = new UIMenuItem("Nearest Job");
            

            UIMenuItem Select_Nearest_Store = new UIMenuItem("Select Nearest Store");
            mainMenu.OnItemSelect += (sender, selectedItem, index) =>
            {
                // Check which item is selected.
                //if (selectedItem == itemSelectFunction)
                //{
                //    GTA.UI.Screen.ShowSubtitle("Hi! I'm testing SimpleUI's OnItemSelect event!");
                //}
                //else 
                if (selectedItem == Active_Mod)
                {
                    // ControlBoolValue is an easy way to let a menu item control
                    // a specific bool with one line of code.
                    // In this example, we will control the var "testBool" with
                    // the "itemBoolControl" menu item.
                    mainMenu.ControlBoolValue(ref Mod_Activated, Active_Mod);



                    if (Mod_Activated)
                    {
                        if (!mainMenu.DisabledList.Contains(Get_CoOrdinates))
                        {
                            //Location details
                            //string path = @"..\Grand Theft Auto V\Scripts\Be A Guide\Locations\Locations.json";
                            //Import_Details.instance.Import_Locations(true, false);

                            //Store Details
                            //path = @"..\Grand Theft Auto V\Scripts\Be A Guide\Locations\Store-Locations.json";
                            //Import_Details.instance.Import_Locations(false, true);                            
                            mainMenu.AddMenuItem(jobTypeList);
                            mainMenu.AddMenuItem(Finalize_Job_Type);                            
                        }
                        else
                        {   
                            mainMenu.ReenableItem(jobTypeList);
                            mainMenu.ReenableItem(Finalize_Job_Type);

                            //mainMenu.ReenableItem(Get_CoOrdinates);
                            //mainMenu.ReenableItem(Nearest_Job_Location);
                            //mainMenu.ReenableItem(Select_Nearest_Store);
                        }
                    }
                    else
                    {   
                        mainMenu.DisableItem(jobTypeList);
                        mainMenu.DisableItem(Finalize_Job_Type);
                        //mainMenu.DisableItem(Get_CoOrdinates);
                        //mainMenu.DisableItem(Nearest_Job_Location);
                        //mainMenu.DisableItem(Select_Nearest_Store);
                    }
                }
                else if (selectedItem == Finalize_Job_Type)
                {

                    //_menuPool.CloseAllMenus();
                    Selection_Manager.instance.SelectedJob = jobTypeList.CurrentListItem.ToString();
                    GTA.UI.Notification.Show("You have selected " + Selection_Manager.instance.SelectedJob + " as job type, Goodluck!");
                    GTA.UI.Notification.Show("Make sure you have enough appropriate supplies before you start the job");

                    subsectionItem2 = new UIMenuSubsectionItem("--- Current Job In Progress : " + Selection_Manager.instance.SelectedJob + " ---");
                    mainMenu.AddMenuItem(subsectionItem2);

                    if (!mainMenu.DisabledList.Contains(Nearest_Job_Location))
                    {
                        mainMenu.AddMenuItem(Get_CoOrdinates);
                        mainMenu.AddMenuItem(Create_New_CheckPoint);
                        mainMenu.AddMenuItem(Nearest_Job_Location);
                        mainMenu.AddMenuItem(Select_Nearest_Store);
                    }
                    else
                    {
                        mainMenu.ReenableItem(Get_CoOrdinates);
                        mainMenu.ReenableItem(Create_New_CheckPoint);
                        mainMenu.ReenableItem(Nearest_Job_Location);
                        mainMenu.ReenableItem(Select_Nearest_Store);
                    }

                    mainMenu.AddMenuItem(Cancle_Job_Type);

                    mainMenu.DisableItem(jobTypeList);
                    mainMenu.DisableItem(Finalize_Job_Type);
                    mainMenu.DisableItem(Active_Mod);


                }
                else if (selectedItem == Cancle_Job_Type)
                {
                    
                    GTA.UI.Notification.Show("You have canclled the current job");

                    if (!mainMenu.DisabledList.Contains(Nearest_Job_Location))
                    {
                        mainMenu.AddMenuItem(Active_Mod);
                        mainMenu.AddMenuItem(jobTypeList);
                        mainMenu.AddMenuItem(Finalize_Job_Type);
                        
                    }
                    else
                    {
                        mainMenu.ReenableItem(Active_Mod);
                        mainMenu.ReenableItem(jobTypeList);
                        mainMenu.ReenableItem(Finalize_Job_Type);
                        
                    }
                    
                    //Disable Items
                    mainMenu.DisableItem(subsectionItem2);
                    mainMenu.DisableItem(Get_CoOrdinates);                    
                    mainMenu.DisableItem(Nearest_Job_Location);
                    mainMenu.DisableItem(Create_New_CheckPoint);
                    mainMenu.DisableItem(Select_Nearest_Store);
                    mainMenu.DisableItem(Cancle_Job_Type);
                }
                else if (selectedItem == Get_CoOrdinates)
                {
                    var position = GTA.Game.Player.Character.Position;
                    GTA.UI.Notification.Show(position.ToString());
                    //Export_Details.instance.ExportFile(position.X, position.Y, position.Z);
                    int _Last_ID = Import_Details.instance.Import(@"..\Grand Theft Auto V\Scripts\Be A Guide\Locations\Locations.json", Export_Details.instance.Job_Locations).Count;                    
                    Core_Job_Type_Model model = new Core_Job_Type_Model {Difficulty_Type = "Easy" , Location = _Last_ID, Mission_Type = Selection_Manager.instance.job_Selection_enum.ToString(), Position_X = GTA.Game.Player.Character.Position.X, Position_Y = GTA.Game.Player.Character.Position.Y, Position_Z = GTA.Game.Player.Character.Position.Z };
                    Export_Details.instance.Export(ref model, @"..\Grand Theft Auto V\Scripts\Be A Guide\Locations\Locations.json");
                    _Last_ID = model.Location + 1;
                }
                else if (selectedItem == Create_New_CheckPoint)
                {
                    var position = GTA.Game.Player.Character.Position;
                    GTA.UI.Notification.Show(position.ToString());
                    GTA.UI.Notification.Show(Selection_Manager.instance.SelectedJob.ToString());
                    string[] folder = {"Sea Diving Instructor","Mount Bicycle Guide","Hiker Guide","Beach ATV Bike Guide","Jet Ski Instructor","Mount ATV Bike Guide","Mount Dirt Bike Guide","Parachute Instructor"};
                    string Parent_Folder = "";

                    switch (Selection_Manager.instance.job_Selection_enum)
                    {
                        case Selection_Manager.Job_Selection.Sea_Diving_Instructor:
                            Parent_Folder = folder[0];
                            break;
                        case Selection_Manager.Job_Selection.Mount_Bicycle_Guide:
                            Parent_Folder = folder[1];
                            break;
                        case Selection_Manager.Job_Selection.Hiker_Guide:
                            Parent_Folder = folder[2];
                            break;
                        case Selection_Manager.Job_Selection.Beach_ATV_Bike_Guide:
                            Parent_Folder = folder[3];
                            break;
                        case Selection_Manager.Job_Selection.Jet_Ski_Instructor:
                            Parent_Folder = folder[4];
                            break;
                        case Selection_Manager.Job_Selection.Mount_ATV_Bike_Guide:
                            Parent_Folder = folder[5];
                            break;
                        case Selection_Manager.Job_Selection.Mount_Dirt_Bike_Guide:
                            Parent_Folder = folder[6];
                            break;
                        case Selection_Manager.Job_Selection.Parachute_Instructor:
                            Parent_Folder = folder[7];
                            break;
                    }

                    if(Checkpoint_List_Handler.Check_Point_List == null)
                    {
                        Checkpoint_List_Handler.Check_Point_List = new List<CheckPoint_Core_Model>();
                    }

                    int _Last_ID = Import_Details.instance.Import(@"..\Grand Theft Auto V\Scripts\Be A Guide\Mission Checkpoints\" + Parent_Folder + @"\Checkpoints.json", Checkpoint_List_Handler.Check_Point_List).Count;
                    CheckpointManager.instance.checkpoint_Location_Name = Selection_Manager.instance.job_Selection_enum.ToString() + "_" + Import_Details.instance.Location_List_Dictionary.FirstOrDefault(x => x.Value == Selection_Manager.instance.Destination).Key;
                    CheckPoint_Core_Model model = new CheckPoint_Core_Model { Checkpoint_Mission_Location = CheckpointManager.instance.checkpoint_Location_Name, Difficulty_Type = "Hard", Checkpoint = _Last_ID, Mission_Type = Selection_Manager.instance.job_Selection_enum.ToString(), Position_X = GTA.Game.Player.Character.Position.X, Position_Y = GTA.Game.Player.Character.Position.Y, Position_Z = GTA.Game.Player.Character.Position.Z };
                    Export_Details.instance.Export(ref model, @"..\Grand Theft Auto V\Scripts\Be A Guide\Mission Checkpoints\" + Parent_Folder + @"\" + @"Checkpoints.json");
                    _Last_ID = model.Checkpoint + 1;
                }
                else if (selectedItem == Nearest_Job_Location)
                {   
                    Import_Details.instance.Import_Locations(true, false);                    
                    Selection_Manager.instance.current_Selection = Selection_Manager.Current_Selection.Job_Selected;
                    Selection_Manager.instance.Destination = World_Manager.instance.Nearest_Location(Import_Details.instance.Location_List_Dictionary, GTA.Game.Player.Character.Position);


                    //Import Checkpoints
                    string[] folder = { "Sea Diving Instructor", "Mount Bicycle Guide", "Hiker Guide", "Beach ATV Bike Guide", "Jet Ski Instructor", "Mount ATV Bike Guide", "Mount Dirt Bike Guide", "Parachute Instructor" };
                    string Parent_Folder = "";

                    switch (Selection_Manager.instance.job_Selection_enum)
                    {
                        case Selection_Manager.Job_Selection.Sea_Diving_Instructor:
                            Parent_Folder = folder[0];
                            break;
                        case Selection_Manager.Job_Selection.Mount_Bicycle_Guide:
                            Parent_Folder = folder[1];
                            break;
                        case Selection_Manager.Job_Selection.Hiker_Guide:
                            Parent_Folder = folder[2];
                            break;
                        case Selection_Manager.Job_Selection.Beach_ATV_Bike_Guide:
                            Parent_Folder = folder[3];
                            break;
                        case Selection_Manager.Job_Selection.Jet_Ski_Instructor:
                            Parent_Folder = folder[4];
                            break;
                        case Selection_Manager.Job_Selection.Mount_ATV_Bike_Guide:
                            Parent_Folder = folder[5];
                            break;
                        case Selection_Manager.Job_Selection.Mount_Dirt_Bike_Guide:
                            Parent_Folder = folder[6];
                            break;
                        case Selection_Manager.Job_Selection.Parachute_Instructor:
                            Parent_Folder = folder[7];
                            break;
                    }

                    Import_Details.instance.Import(@"..\Grand Theft Auto V\Scripts\Be A Guide\Mission Checkpoints\" + Parent_Folder + @"\Checkpoints.json", Checkpoint_List_Handler.Check_Point_List);
                    CheckpointManager.instance.checkpoint_Location_Name = Selection_Manager.instance.job_Selection_enum.ToString() + "_" + Import_Details.instance.Location_List_Dictionary.FirstOrDefault(x => x.Value == Selection_Manager.instance.Destination).Key;
                    CheckpointManager.instance.Collect_CheckPoints(Checkpoint_List_Handler.Check_Point_List);

                    if (Selection_Manager.instance.Destination != Vector3.Zero)
                    {
                        World_Manager.instance.Activate_Waypoint(Selection_Manager.instance.Destination);
                    }
                }
                
                else if (selectedItem == Select_Nearest_Store)
                {   
                    Import_Details.instance.Import_Locations(false, true);

                    //Selection
                    Selection_Manager.instance.current_Selection = Selection_Manager.Current_Selection.Shop_Selected;
                    Selection_Manager.instance.Destination = World_Manager.instance.Nearest_Location(Import_Details.instance.Shop_List_Dictionary, GTA.Game.Player.Character.Position);

                    //Hiking Shop
                    //{"Location 5":[1733.69177,6415.0376,35.03722]}
                    if (Selection_Manager.instance.Destination != Vector3.Zero)
                    {
                        World_Manager.instance.Activate_Waypoint(Selection_Manager.instance.Destination);
                    }

                    //TODO
                    //Deep Sea Diving Instructor Shop

                    //Export_Details.instance.ExportFile(position.X, position.Y, position.Z);
                    Purchase_Menu.Purchase_Menu.instance.InitMenu();


                }
                else if (selectedItem == itemAddPerson)
                {
                    string fname = Game.GetUserInput("FirstName");
                    if (String.IsNullOrWhiteSpace(fname)) return;

                    string lname = Game.GetUserInput("LastName");
                    if (String.IsNullOrWhiteSpace(lname)) return;

                    string input = Game.GetUserInput("ID");
                    if (String.IsNullOrWhiteSpace(lname)) return;

                    int id;
                    bool idParsed = int.TryParse(input, out id);

                    if (!idParsed) return;

                    //testListAdvanced.Add(new Person(fname, lname, id));

                    // Call this after modifying your list or you may
                    // get an out of bounds error.
                    itemListControlAdvanced.SaveListUpdateFromOutOfBounds();

                    GTA.UI.Screen.ShowSubtitle(fname + " " + lname + " added to list!");
                }
                else if (selectedItem == itemRemoveLastPerson)
                {
                    if (testListAdvanced.Count > 1)
                    {
                        GTA.UI.Screen.ShowSubtitle(testListAdvanced[testListAdvanced.Count - 1].ToString() + " removed from list!");

                        // Don't want to use LINQ for just this one line..
                        testListAdvanced.RemoveAt(testListAdvanced.Count - 1);

                        itemListControlAdvanced.SaveListUpdateFromOutOfBounds();
                    }
                    else
                    {
                        GTA.UI.Screen.ShowSubtitle("There is only one person left!");
                    }
                }
            };

            // Let's subscribe subMenu's WhileItemHighlight event to an anonymous method
            // This method will be executed continuously while subMenu is open.
            subMenu.WhileItemHighlight += (sender, selectedItem, index) =>
            {
                // Check which item is selected.
                if (selectedItem == submenuItem1)
                {
                    GTA.UI.Screen.ShowSubtitle("Highlighting subMenu's Item 1");
                }
                else if (selectedItem == submenuItem2)
                {
                    GTA.UI.Screen.ShowSubtitle("Highlighting subMenu's Item 2");
                }
            };

            // Let's subscribe mainMenu's OnItemLeftRight event to the method
            // "MainMenu_OnItemLeftRight"
            // This method will then be executed whenever you press left or right
            // while mainMenu is open.
            mainMenu.OnItemLeftRight += MainMenu_OnItemLeftRight;

            // That's it for this example setup!
            // SimpleUI also supports scrolling, so you can add as many items
            // or submenus as you'd like.
            // SimpleUI also supports dynamic hiding/showing of menu items,
            // and Dispose methods for items and menus, allowing easy modification
            // after the initial setup. Explore using Intellisense!
        }

        private void MainMenu_OnItemLeftRight(UIMenu sender, UIMenuItem selectedItem, int index, UIMenu.Direction direction)
        {
            // Check which item is selected.
            if (selectedItem == itemIntegerControl)
            {
                // ControlIntValue is an easy way to let a menu item control
                // a specific int with one line of code.
                // In this example, we will control the var "testInt" with
                // the "itemIntegerControl" menu item.
                // The params that follow are explained with intellisense.
                mainMenu.ControlIntValue(ref testInt, itemIntegerControl, direction, 1, 5, true, 0, 100);

                GTA.UI.Screen.ShowSubtitle("You pressed " + (direction == UIMenu.Direction.Left ? "Left" : "Right") + " while highlighting Integer Item!");
            }
            else if (selectedItem == itemFloatControl)
            {
                // ControlFloatValue is an easy way to let a menu item control
                // a specific float with one line of code.
                // In this example, we will control the var "testFloat" with
                // the "itemFloatControl" menu item.
                // The params that follow are explained with intellisense.
                mainMenu.ControlFloatValue(ref testFloat, itemFloatControl, direction, 0.5f, 1f, 2, true, 0f, 10f);

                GTA.UI.Screen.ShowSubtitle("You pressed " + (direction == UIMenu.Direction.Left ? "Left" : "Right") + " while highlighting Float Item!");
            }
            else if (selectedItem == Job_Type)
            {
                // ControlEnumValue is an easy way to let a menu item control
                // a specific enum with one line of code.
                // In this example, we will control the var "testEnum" with
                // the "itemEnumControl" menu item.
                mainMenu.ControlEnumValue(ref Selection_Manager.instance.job_Selection_enum, Job_Type, direction);
                GTA.UI.Screen.ShowSubtitle("Job type selected : " + Selection_Manager.instance.job_Selection_enum);
                //GTA.UI.Screen.ShowSubtitle("You pressed " + (direction == UIMenu.Direction.Left ? "Left" : "Right") + " while highlighting Enum Item!");
            }
            else if (selectedItem == jobTypeList)
            {
                // An item of type UIMenuListItem is automatically controlled by the menu.
                //GTA.UI.Notification.Show();

                //jobTypeList.IndexFromItem(jobTypeList.CurrentListItem).ToString();
                //YourEnum foo = (YourEnum)yourInt;
                Selection_Manager.instance.job_Selection_enum = (Selection_Manager.Job_Selection)jobTypeList.IndexFromItem(jobTypeList.CurrentListItem)+1;
                Selection_Manager.instance.SelectedJob = jobTypeList.CurrentListItem.ToString();
                GTA.UI.Screen.ShowSubtitle("\"" + jobTypeList.CurrentListItem.ToString() + "\" is selected.");
            }
            else if (selectedItem == itemListControlAdvanced)
            {
                // UIMenuListItem.CurrentListItem will return the actual selected object
                // in the list. You must cast it to the actual object type. Ex:
                // Person p = (Person)list.CurrentListItem;
                GTA.UI.Screen.ShowSubtitle("\"" + itemListControlAdvanced.CurrentListItem.ToString() + "\" is selected.");
            }
        }


    }//Main_Menu_UI class ends here

}//Namespace ends here
