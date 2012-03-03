namespace HaCreator.GUI
{
    partial class YesNoBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
            this.reflectionImage1 = new DevComponents.DotNetBar.Controls.ReflectionImage();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // styleManager
            // 
            this.styleManager.ManagerColorTint = System.Drawing.SystemColors.HotTrack;
            this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007VistaGlass;
            // 
            // reflectionImage1
            // 
            // 
            // 
            // 
            this.reflectionImage1.BackgroundStyle.Class = "";
            this.reflectionImage1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.reflectionImage1.Dock = System.Windows.Forms.DockStyle.Left;
            this.reflectionImage1.Image = global::HaCreator.Properties.Resources.sleep_3;
            this.reflectionImage1.Location = new System.Drawing.Point(0, 0);
            this.reflectionImage1.Name = "reflectionImage1";
            this.reflectionImage1.Size = new System.Drawing.Size(99, 111);
            this.reflectionImage1.TabIndex = 0;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelX1.Location = new System.Drawing.Point(99, 0);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(303, 90);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "labelX1";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonX1.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonX1.Location = new System.Drawing.Point(99, 90);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(104, 21);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 2;
            this.buttonX1.Text = "Yes";
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.DialogResult = System.Windows.Forms.DialogResult.No;
            this.buttonX2.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonX2.Location = new System.Drawing.Point(298, 90);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(104, 21);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 3;
            this.buttonX2.Text = "No";
            // 
            // YesNoBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 111);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.reflectionImage1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "YesNoBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Title";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager;
        private DevComponents.DotNetBar.Controls.ReflectionImage reflectionImage1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
    }
}