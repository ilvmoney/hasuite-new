namespace HaCreator.GUI
{
    partial class VREditor
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
            this.doneBtn = new DevComponents.DotNetBar.ButtonX();
            this.VRIncrement = new DevComponents.Editors.IntegerInput();
            this.resetBtn = new DevComponents.DotNetBar.ButtonX();
            this.label1 = new System.Windows.Forms.Label();
            this.VRRight = new DevComponents.DotNetBar.ButtonX();
            this.VRBottom = new DevComponents.DotNetBar.ButtonX();
            this.VRLeft = new DevComponents.DotNetBar.ButtonX();
            this.VRUp = new DevComponents.DotNetBar.ButtonX();
            this.miniMap = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.VRIncrement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.miniMap)).BeginInit();
            this.SuspendLayout();
            // 
            // styleManager
            // 
            this.styleManager.ManagerColorTint = System.Drawing.SystemColors.HotTrack;
            this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007VistaGlass;
            // 
            // doneBtn
            // 
            this.doneBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.doneBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.doneBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.doneBtn.Location = new System.Drawing.Point(0, 214);
            this.doneBtn.Name = "doneBtn";
            this.doneBtn.Size = new System.Drawing.Size(363, 23);
            this.doneBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.doneBtn.TabIndex = 1;
            this.doneBtn.Text = "Done";
            this.doneBtn.Click += new System.EventHandler(this.doneBtn_Click);
            // 
            // VRIncrement
            // 
            this.VRIncrement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.VRIncrement.BackgroundStyle.Class = "DateTimeInputBackground";
            this.VRIncrement.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.VRIncrement.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.VRIncrement.Location = new System.Drawing.Point(275, 54);
            this.VRIncrement.MaxValue = 10;
            this.VRIncrement.MinValue = -10;
            this.VRIncrement.Name = "VRIncrement";
            this.VRIncrement.ShowUpDown = true;
            this.VRIncrement.Size = new System.Drawing.Size(37, 20);
            this.VRIncrement.TabIndex = 8;
            this.VRIncrement.Value = 1;
            // 
            // resetBtn
            // 
            this.resetBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.resetBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resetBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.resetBtn.Location = new System.Drawing.Point(232, 125);
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.Size = new System.Drawing.Size(123, 23);
            this.resetBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.resetBtn.TabIndex = 9;
            this.resetBtn.Text = "Reset";
            this.resetBtn.Click += new System.EventHandler(this.resetBtn_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 183);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(339, 28);
            this.label1.TabIndex = 10;
            this.label1.Text = "Warning: Please use the VR editor carefully, it might not be accurate!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VRRight
            // 
            this.VRRight.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.VRRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.VRRight.BackColor = System.Drawing.Color.Transparent;
            this.VRRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.VRRight.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.VRRight.Image = global::HaCreator.Properties.Resources.arrow_right;
            this.VRRight.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.VRRight.Location = new System.Drawing.Point(318, 47);
            this.VRRight.Name = "VRRight";
            this.VRRight.Shape = new DevComponents.DotNetBar.EllipticalShapeDescriptor();
            this.VRRight.Size = new System.Drawing.Size(37, 33);
            this.VRRight.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.VRRight.TabIndex = 7;
            this.VRRight.Click += new System.EventHandler(this.VRRight_Click);
            // 
            // VRBottom
            // 
            this.VRBottom.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.VRBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.VRBottom.BackColor = System.Drawing.Color.Transparent;
            this.VRBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.VRBottom.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.VRBottom.Image = global::HaCreator.Properties.Resources.arrow_down;
            this.VRBottom.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.VRBottom.Location = new System.Drawing.Point(275, 86);
            this.VRBottom.Name = "VRBottom";
            this.VRBottom.Shape = new DevComponents.DotNetBar.EllipticalShapeDescriptor();
            this.VRBottom.Size = new System.Drawing.Size(37, 33);
            this.VRBottom.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.VRBottom.TabIndex = 5;
            this.VRBottom.Click += new System.EventHandler(this.VRBottom_Click);
            // 
            // VRLeft
            // 
            this.VRLeft.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.VRLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.VRLeft.BackColor = System.Drawing.Color.Transparent;
            this.VRLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.VRLeft.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.VRLeft.Image = global::HaCreator.Properties.Resources.arrow_left;
            this.VRLeft.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.VRLeft.Location = new System.Drawing.Point(232, 47);
            this.VRLeft.Name = "VRLeft";
            this.VRLeft.Shape = new DevComponents.DotNetBar.EllipticalShapeDescriptor();
            this.VRLeft.Size = new System.Drawing.Size(37, 33);
            this.VRLeft.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.VRLeft.TabIndex = 4;
            this.VRLeft.Click += new System.EventHandler(this.VRLeft_Click);
            // 
            // VRUp
            // 
            this.VRUp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.VRUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.VRUp.BackColor = System.Drawing.Color.Transparent;
            this.VRUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.VRUp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.VRUp.Image = global::HaCreator.Properties.Resources.arrow_up;
            this.VRUp.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.VRUp.Location = new System.Drawing.Point(275, 8);
            this.VRUp.Name = "VRUp";
            this.VRUp.Shape = new DevComponents.DotNetBar.EllipticalShapeDescriptor();
            this.VRUp.Size = new System.Drawing.Size(37, 33);
            this.VRUp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.VRUp.TabIndex = 3;
            this.VRUp.Click += new System.EventHandler(this.VRUp_Click);
            // 
            // miniMap
            // 
            this.miniMap.Image = global::HaCreator.Properties.Resources.RegenMinimap;
            this.miniMap.Location = new System.Drawing.Point(12, 8);
            this.miniMap.Name = "miniMap";
            this.miniMap.Size = new System.Drawing.Size(63, 32);
            this.miniMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.miniMap.TabIndex = 0;
            this.miniMap.TabStop = false;
            this.miniMap.Paint += new System.Windows.Forms.PaintEventHandler(this.miniMap_Paint);
            // 
            // VREditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 237);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.resetBtn);
            this.Controls.Add(this.VRIncrement);
            this.Controls.Add(this.VRRight);
            this.Controls.Add(this.VRBottom);
            this.Controls.Add(this.VRLeft);
            this.Controls.Add(this.VRUp);
            this.Controls.Add(this.doneBtn);
            this.Controls.Add(this.miniMap);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(379, 275);
            this.Name = "VREditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VREditor";
            ((System.ComponentModel.ISupportInitialize)(this.VRIncrement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.miniMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager;
        private System.Windows.Forms.PictureBox miniMap;
        private DevComponents.DotNetBar.ButtonX doneBtn;
        private DevComponents.DotNetBar.ButtonX VRUp;
        private DevComponents.DotNetBar.ButtonX VRLeft;
        private DevComponents.DotNetBar.ButtonX VRBottom;
        private DevComponents.DotNetBar.ButtonX VRRight;
        private DevComponents.Editors.IntegerInput VRIncrement;
        private DevComponents.DotNetBar.ButtonX resetBtn;
        private System.Windows.Forms.Label label1;
    }
}