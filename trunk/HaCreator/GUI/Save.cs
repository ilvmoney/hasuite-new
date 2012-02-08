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
    public partial class Save : DevComponents.DotNetBar.Office2007Form
    {
        public Save()
        {
            InitializeComponent();
            styleManager.ManagerStyle = UserSettings.applicationStyle;
        }

        private void Save_Load(object sender, EventArgs e)
        {
            //textDir.Text = ApplicationSettings.MapleFolder + "\\EdittedWZ";
        }

        /*private void browseBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fldrBrowse = new FolderBrowserDialog();
            fldrBrowse.Description = "Browse to your MapleStory folder";
            if (fldrBrowse.ShowDialog() != DialogResult.OK) return;
            textDir.Text = fldrBrowse.SelectedPath + "\\EdittedWZ";
        }*/

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (Program.WzManager.getWzPath() != "ERROR")
            {
                DialogResult msgbx = MessageBox.Show("Are you sure you want to save Map.wz?", "Save confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgbx == DialogResult.Yes)
                {
                  Program.WzManager.SaveMap(Program.WzManager.getWzPath() + "\\EdittedWZ"); //Whoops, forgot to add in EdittedWZ
                }
            
            } /*else {
                MessageBox.Show("Some shiz went down!" ,"FFFFFFFFFFFUUUUUUUUUUUUUU-", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }
    }
}
