using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HaCreator.GUI
{
    public partial class YesNoBox : DevComponents.DotNetBar.Office2007Form
    {

        //public string result = "Waiting";
        /// <summary>
        /// Show's a box with a yes-type button, and a no-type button
        /// </summary>
        /// <param name="title">Title of the form</param>
        /// <param name="msg">Message to display</param>
        /// <param name="yText">'Yes button' text</param>
        /// <param name="nText">'No button' text</param>
        public YesNoBox(string title, string msg, string yText = "Yes", string nText = "No")
        {
            InitializeComponent();
            styleManager.ManagerStyle = UserSettings.applicationStyle;
            this.Text = title;
            labelX1.Text = msg;
            buttonX1.Text = yText;
            buttonX2.Text = nText;
            this.ShowDialog();
        }

        private void YesNoBox_Load(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

        }
    }
}
