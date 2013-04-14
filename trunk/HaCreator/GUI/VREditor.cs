using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HaCreator.MapEditor;

namespace HaCreator.GUI
{
    public partial class VREditor : DevComponents.DotNetBar.Office2007Form
    {
        Board mapBoard = null;
        Rectangle VRChanged = new Rectangle(0, 0, 0, 0);
        Rectangle VR = new Rectangle(0, 0, 0, 0);
        Rectangle VROld = new Rectangle(0, 0, 0, 0);
        public VREditor(Board mapBoardd)
        {
            InitializeComponent();
            styleManager.ManagerStyle = UserSettings.applicationStyle;
            mapBoard = mapBoardd;
            VROld = (Rectangle)mapBoard.MapInfo.VR;
            miniMap.Image = mapBoardd.MiniMap;
            miniMap.Update();
            miniMap.Refresh();


            //Rectangle VR = new Rectangle(miniMap.Location.X - (mapBoard.MapInfo.VR.Value.X / 4), miniMap.Location.Y - (mapBoard.MapInfo.VR.Value.Y / 4), miniMap.Width - (mapBoard.MapInfo.VR.Value.Width / 4), miniMap.Height - (mapBoard.MapInfo.VR.Value.Height / 4));
            
            //VR.Location = miniMap.Location;
        }

        private void miniMap_Paint(object sender, PaintEventArgs e)
        {
            if (mapBoard != null)
            {
                Pen pen = new Pen(Color.Blue, 1);
                VR = new Rectangle(((mapBoard.MapInfo.VR.Value.X / miniMap.Width) + (miniMap.Location.X)) + VRChanged.X, ((mapBoard.MapInfo.VR.Value.Y / (miniMap.Height * 4)) + (miniMap.Location.Y)) + VRChanged.Y, ((mapBoard.MapInfo.VR.Value.Width / miniMap.Location.X) - (mapBoard.MiniMap.Width / 4) - miniMap.Location.X) + VRChanged.Width, ((mapBoard.MapInfo.VR.Value.Height / miniMap.Location.Y) - (mapBoard.MiniMap.Height) - miniMap.Location.Y) + VRChanged.Height);
                e.Graphics.DrawRectangle(pen, VR);
            }
        }

        private void VRUp_Click(object sender, EventArgs e)
        {
            VRChanged.Y += VRIncrement.Value;
            miniMap.Update();
            miniMap.Refresh();
        }

        private void VRLeft_Click(object sender, EventArgs e)
        {
            VRChanged.X += VRIncrement.Value;
            miniMap.Update();
            miniMap.Refresh();
        }

        private void VRRight_Click(object sender, EventArgs e)
        {
            VRChanged.Width += VRIncrement.Value;
            miniMap.Update();
            miniMap.Refresh();
        }

        private void VRBottom_Click(object sender, EventArgs e)
        {
            VRChanged.Height += VRIncrement.Value;
            miniMap.Update();
            miniMap.Refresh();
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            VRChanged = new Rectangle(0, 0, 0, 0);
            miniMap.Update();
            miniMap.Refresh();
        }

        private void doneBtn_Click(object sender, EventArgs e)
        {
            //VR = new Rectangle(((mapBoard.MapInfo.VR.Value.X / miniMap.Width) + (miniMap.Location.X)) + VRChanged.X, ((mapBoard.MapInfo.VR.Value.Y / (miniMap.Height * 4)) + (miniMap.Location.Y)) + VRChanged.Y, ((mapBoard.MapInfo.VR.Value.Width / miniMap.Location.X) - (mapBoard.MiniMap.Width / 4) - miniMap.Location.X) + VRChanged.Width, ((mapBoard.MapInfo.VR.Value.Height / miniMap.Location.Y) - (mapBoard.MiniMap.Height) - miniMap.Location.Y) + VRChanged.Height);
            //Rectangle newVR = new Rectangle((VR.X * miniMap.Width) - (miniMap.Location.X), (VR.Y * (miniMap.Height / 4)) - miniMap.Location.Y, (VR.Width * miniMap.Location.X) + (mapBoard.MiniMap.Width * 4) + miniMap.Location.X, (VR.Height * miniMap.Location.Y) + mapBoard.MiniMap.Height + miniMap.Location.Y);
            Rectangle newVR = new Rectangle(VROld.X + (VRChanged.X * mapBoard.mag), VROld.Y + (VRChanged.Y * mapBoard.mag), VROld.Width + (VRChanged.Width * mapBoard.mag), VROld.Height + (VRChanged.Height * mapBoard.mag));
            mapBoard.MapInfo.VR = newVR;
            VRChanged = new Rectangle(0, 0, 0, 0);
            VR = new Rectangle(0, 0, 0, 0);
            mapBoard.ParentControl.RenderFrame();
            this.Close();
        }
    }
}
