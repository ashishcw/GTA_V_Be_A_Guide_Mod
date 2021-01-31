using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using GTA.UI;
using GTA_V_Be_A_Guide_Mod.Data.Import;
using GTA_V_Be_A_Guide_Mod.Manager.CheckpointManager;
using GTA_V_Be_A_Guide_Mod.Manager.SelectionManager;
using GTA_V_Be_A_Guide_Mod.Manager.World;
using GTA_V_Be_A_Guide_Mod.SimpleUIMenu.MainMenu;
using GTA_V_Be_A_Guide_Mod.SimpleUIMenu.Purchase_Menu;
using GTA_V_Be_A_Guide_Mod.UI.ScreenOverLay;

namespace GTA_V_Be_A_Guide_Mod
{

    public class Main : Script
    {  
        internal bool purchasable_Menu_Can_Activate = false, job_Activated = false;
        //GTA.UI.CustomSprite temp = new GTA.UI.CustomSprite(@"..\Grand Theft Auto V\Scripts\Be A Guide\UI\Basic bars & buttons\Medkits\5 Med kit bar\5 Med kit full.png", new SizeF(180, 30), new PointF(1100f, 500f));
        //GTA.UI.CustomSprite temp, temp2, temp3, temp4;
        List<GTA.UI.CustomSprite> customSprites_List = new List<GTA.UI.CustomSprite>();

        bool draw_NOW = false;
        int target_Time = Game.GameTime + 5000;
        //public Vector3 Destination = Vector3.Zero;

        int tempCounter = 0;

        public Main()
        {
            Tick += OnTick;
            KeyUp += OnKeyUp;
            Aborted += Main_Aborted;

            Interval = 0;

            //Main Menu Refrence            
            GTA.UI.Notification.Show("Be a Tour Guide - 1.0");

            GTA.Game.Player.Character.BlockPermanentEvents = true;

            Group_Bar_Init();
        }

        private void Main_Aborted(object sender, EventArgs e)
        {
            //foreach (var item in CheckpointManager.instance.Created_CheckPoints)
            //{
            //    CheckpointManager.instance.Created_CheckPoints.Remove(item.Key);
            //}

            for (int i = 0; i < CheckpointManager.instance.created_Checkpoints_Dictionary.Count; i++)
            {
                CheckpointManager.instance.created_Checkpoints_Dictionary.Clear();
                break;
            }
        }

        void OnTick(object sender, EventArgs e)
        {
            // Process all the menus in _menuPool
            Main_Menu.instance._menuPool.ProcessMenus();

            if (draw_NOW)
            {
                if (Game.GameTime > target_Time)
                {
                    Random rnd = new Random();

                    var randomnumber = rnd.Next(10);

                    if(randomnumber > 5)
                    {
                        customSprites_List[0] = Base_UI_Handler.instance.Update_Sprite(true, false, Base_UI_Handler.instance.Custom_Sprite_Collection_Hearts_Satisfaction, customSprites_List[0]);
                        customSprites_List[1] = Base_UI_Handler.instance.Update_Sprite(true, false, Base_UI_Handler.instance.Custom_Sprite_Collection_MedKit, customSprites_List[1]);
                        customSprites_List[2] = Base_UI_Handler.instance.Update_Sprite(true, false, Base_UI_Handler.instance.Custom_Sprite_Collection_Energy, customSprites_List[2]);
                        customSprites_List[3] = Base_UI_Handler.instance.Update_Sprite(true, false, Base_UI_Handler.instance.Custom_Sprite_Collection_Rating, customSprites_List[3]);
                    }
                    else
                    {
                        customSprites_List[0] = Base_UI_Handler.instance.Update_Sprite(false, true, Base_UI_Handler.instance.Custom_Sprite_Collection_Hearts_Satisfaction, customSprites_List[0]);
                        customSprites_List[1] = Base_UI_Handler.instance.Update_Sprite(false, true, Base_UI_Handler.instance.Custom_Sprite_Collection_MedKit, customSprites_List[1]);
                        customSprites_List[2] = Base_UI_Handler.instance.Update_Sprite(false, true, Base_UI_Handler.instance.Custom_Sprite_Collection_Energy, customSprites_List[2]);
                        customSprites_List[3] = Base_UI_Handler.instance.Update_Sprite(false, true, Base_UI_Handler.instance.Custom_Sprite_Collection_Rating, customSprites_List[3]);
                    }

                    target_Time = Game.GameTime + 5000;
                }

                for (int i = 0; i < customSprites_List.Count; i++)
                {
                    if(customSprites_List[i] != null)
                    {
                        customSprites_List[i].Draw();
                    }
                }
            }


            if (Main_Menu.instance.Mod_Activated)
            {
                if (purchasable_Menu_Can_Activate)
                {
                    Purchase_Menu.instance.purchase_MenuPool.ProcessMenus();
                }
                
                //World_Manager events

                //Display Location Marker
                if (Selection_Manager.instance.current_Selection == Selection_Manager.Current_Selection.None)
                {

                }else if (Selection_Manager.instance.current_Selection == Selection_Manager.Current_Selection.Job_Selected)
                {   
                    
                    if (World.GetDistance(Selection_Manager.instance.Destination, GTA.Game.Player.Character.Position) <= 20f)
                    {
                        if (!job_Activated)
                        {
                            World_Manager.instance.World_Location_Marker_Display(false, Selection_Manager.instance.Destination);
                        }
                        
                    }
                    else
                    {   
                        if (job_Activated)
                        {   
                            if (CheckpointManager.instance.created_Checkpoints_Dictionary.Count != CheckpointManager.instance.completed_CheckPoints_Dictionary.Count)
                            {
                                CheckpointManager.instance.Check_Current_CheckPointStatus(CheckpointManager.instance.created_Checkpoints_Dictionary);
                            }
                            else
                            {
                                GTA.Audio.ReleaseSound(GTA.Audio.PlaySoundFrontend("BASE_JUMP_PASSED", "HUD_AWARDS"));
                                CheckpointManager.instance.Clear_All_Checkpoint_Lists_Dictionaries();
                                job_Activated = false;
                                GTA.UI.Notification.Show("Completed");
                            }
                        }
                    }
                }else if (Selection_Manager.instance.current_Selection == Selection_Manager.Current_Selection.Shop_Selected)
                {
                    if (World.GetDistance(Selection_Manager.instance.Destination, GTA.Game.Player.Character.Position) <= 20f)
                    {
                        World_Manager.instance.World_Location_Marker_Display(false, Selection_Manager.instance.Destination);

                        if (World.GetDistance(Selection_Manager.instance.Destination, GTA.Game.Player.Character.Position) < 5f)
                        {
                            if (!purchasable_Menu_Can_Activate)
                            {
                                purchasable_Menu_Can_Activate = true;
                            }
                        }
                        else
                        {
                            if (purchasable_Menu_Can_Activate)
                            {
                                purchasable_Menu_Can_Activate = false;
                            }
                        }
                    }
                }
            }
        }

        void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                // Open / close the menu with the K key.
                Main_Menu.instance._menuPool.OpenCloseLastMenu();
            }

            if (Main_Menu.instance.Mod_Activated)
            {
                if (purchasable_Menu_Can_Activate)
                {
                    if(e.KeyCode == Keys.E)
                    {
                        Purchase_Menu.instance.purchase_MenuPool.OpenCloseLastMenu();
                    }
                }
            }


            if (e.KeyCode == Keys.L)
            {
                this.draw_NOW = !this.draw_NOW;
                
            }
            
            if(e.KeyCode == Keys.K)
            {
                if(CheckpointManager.instance.created_Checkpoints_Dictionary.Count > 0)
                {
                    foreach (var item in CheckpointManager.instance.created_Checkpoints_Dictionary)
                    {
                        var value = CheckpointManager.instance.created_Checkpoints_Dictionary[item.Key];
                        value.Delete();                        
                    }

                }
            }

            if (e.KeyCode == Keys.O)
            {
                job_Activated = true;
                
                CheckpointManager.instance.Create_CheckPoint(CheckpointManager.instance.checkPoint_Final_List);
                
            }
        }

        private void Group_Bar_Init()
        {
            GTA.UI.CustomSprite temp = new GTA.UI.CustomSprite(@"..\Grand Theft Auto V\Scripts\Be A Guide\UI\Base Sprites\GroupSatisfaction\18.png", new SizeF(180, 30), new PointF(1100f, 500f));
            customSprites_List.Clear();

            temp = Base_UI_Handler.instance.Update_Sprite(false, false, Base_UI_Handler.instance.Custom_Sprite_Collection_Hearts_Satisfaction, temp);
            customSprites_List.Add(temp);            

            temp = Base_UI_Handler.instance.Update_Sprite(false, false, Base_UI_Handler.instance.Custom_Sprite_Collection_MedKit, temp);
            customSprites_List.Add(temp);            

            temp = Base_UI_Handler.instance.Update_Sprite(false, false, Base_UI_Handler.instance.Custom_Sprite_Collection_Energy, temp);
            customSprites_List.Add(temp);            

            temp = Base_UI_Handler.instance.Update_Sprite(false, false, Base_UI_Handler.instance.Custom_Sprite_Collection_Rating, temp);
            customSprites_List.Add(temp);            

        }

        public static void DrawSprite(string dictionary, string texture, PointF position, SizeF size, float heading, Color color)
        {
            var resolution = GetScreenResolution(out var a);
            var offset = GetOffsetForAspectRatio(a);

            Function.Call(Hash.DRAW_SPRITE, dictionary, texture, position.X / resolution.Width + offset,
                position.Y / resolution.Height + offset, size.Width / resolution.Width, size.Height / resolution.Height,
                heading, color.R, color.G, color.B, color.A);
        }

        public static Size GetScreenResolution(out float aspectRatio)
        {
            var ratio = GetAspectRatio();
            aspectRatio = ratio;
            const int height = 1280;
            var newWidth = height * ratio;
            return new Size((int)newWidth, height);
        }

        private static float GetAspectRatio()
        {
            return Function.Call<float>(Hash._GET_ASPECT_RATIO, 1); // _GET_ASPECT_RATIO
        }

        private static float GetOffsetForAspectRatio(float aspect)
        {
            var zone = Function.Call<float>(Hash.GET_SAFE_ZONE_SIZE);
            var safeZone = 1 - zone;
            safeZone *= 0.5f;
            if (aspect <= 1.77777777778f)
                return safeZone;
            var o = 1f - 1.7777777910232544f / aspect;
            o *= 0.5f;
            return o + safeZone;
        }
    }

}//Namespace ends here
