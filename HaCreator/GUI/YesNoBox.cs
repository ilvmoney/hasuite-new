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
        public YesNoBox(string title, string msg, string yText, string nText)
        {
            InitializeComponent();
            styleManager.ManagerStyle = UserSettings.applicationStyle;
            this.Text = title;
            labelX1.Text = msg;
            buttonX1.Text = yText;
            buttonX2.Text = nText;
        }
    }
}
