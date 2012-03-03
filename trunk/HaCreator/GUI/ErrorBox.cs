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
        public ErrorBox(string errmsg)
        {
            InitializeComponent();
            styleManager.ManagerStyle = UserSettings.applicationStyle;
            labelX1.Text = errmsg;
        }

        private void ErrorBox_Load(object sender, EventArgs e)
        {

        }
    }
}
