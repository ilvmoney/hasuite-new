using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HaCreator.MapEditor;
using System.IO;

namespace HaCreator.GUI
{
    public partial class Save : DevComponents.DotNetBar.Office2007Form
    {
        private MultiBoard multiBoard;

        public Save(MultiBoard multiBoard)
        {
            InitializeComponent();
            styleManager.ManagerStyle = UserSettings.applicationStyle;
            this.multiBoard = multiBoard;
            label1.Text = "This will save Map.wz to:\r\n'" + Program.WzManager.getWzPath() + "\\EdittedWZ' (Please make sure that the directory exists)";
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
                Form msgbx = new GUI.YesNoBox("Save confirmation", "Are you sure you want to save Map.wz?", "Yes", "No");
                //DialogResult msgbx = MessageBox.Show("Are you sure you want to save Map.wz?", "Save confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (msgbx == DialogResult.Yes)
                if (msgbx.DialogResult == DialogResult.Yes)
                {
                    progressBar1.Enabled = true;
                    progressBar1.PerformStep(); //10%
                    progressBar1.Refresh();
                    if (MapEditor.Saver.WriteToMap(multiBoard.SelectedBoard) == "cancel")
                    {
                        progressBar1.Value = progressBar1.Minimum;
                        progressBar1.Refresh();
                        progressBar1.Update();
                        return;
                    }
                    if (!Directory.Exists(Program.WzManager.getWzPath() + "\\EdittedWZ"))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(Program.WzManager.getWzPath() + "\\EdittedWZ");
                    }
                    Program.WzManager.SaveMap(Program.WzManager.getWzPath() + "\\EdittedWZ", multiBoard.SelectedBoard.MapInfo.mapImage, progressBar1);
                    //MessageBox.Show("Save completed!", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    new GUI.InfoMsgBox("Completed", "Save completed!");
                }
            
            } /*else {
                MessageBox.Show("Some shiz went down!" ,"FFFFFFFFFFFUUUUUUUUUUUUUU-", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
