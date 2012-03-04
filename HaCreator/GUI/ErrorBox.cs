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
    public partial class ErrorBox : DevComponents.DotNetBar.Office2007Form
    {

        /// <summary>
        /// Show's an error box
        /// </summary>
        /// <param name="errmsg">Error message to be displayed</param>
        public ErrorBox(string errmsg)
        {
            InitializeComponent();
            styleManager.ManagerStyle = UserSettings.applicationStyle;
            labelX1.Text = errmsg;
            this.ShowDialog();
        }

        private void ErrorBox_Load(object sender, EventArgs e)
        {

        }

        private void labelX1_Click(object sender, EventArgs e)
        {

        }
    }
}
