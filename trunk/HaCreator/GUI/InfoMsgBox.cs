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
    public partial class InfoMsgBox : DevComponents.DotNetBar.Office2007Form
    {

        /// <summary>
        /// Shows an information box
        /// </summary>
        /// <param name="title">The title of the form</param>
        /// <param name="msg">The message to display</param>
        public InfoMsgBox(string title, string msg)
        {
            InitializeComponent();
            styleManager.ManagerStyle = UserSettings.applicationStyle;
            this.Text = title;
            labelX1.Text = msg;
            this.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
