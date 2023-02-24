namespace CanteenDashboard
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.cmbRoom = new MaterialSkin.Controls.MaterialComboBox();
            this.selectBtn = new MaterialSkin.Controls.MaterialButton();
            this.SuspendLayout();
            // 
            // cmbRoom
            // 
            this.cmbRoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRoom.AutoResize = false;
            this.cmbRoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbRoom.Depth = 0;
            this.cmbRoom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbRoom.DropDownHeight = 174;
            this.cmbRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoom.DropDownWidth = 121;
            this.cmbRoom.Font = new System.Drawing.Font("Roboto Medium", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbRoom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbRoom.FormattingEnabled = true;
            this.cmbRoom.Hint = "Select Room";
            this.cmbRoom.IntegralHeight = false;
            this.cmbRoom.ItemHeight = 43;
            this.cmbRoom.Location = new System.Drawing.Point(21, 124);
            this.cmbRoom.MaxDropDownItems = 4;
            this.cmbRoom.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbRoom.Name = "cmbRoom";
            this.cmbRoom.Size = new System.Drawing.Size(289, 49);
            this.cmbRoom.StartIndex = 0;
            this.cmbRoom.TabIndex = 0;
            // 
            // selectBtn
            // 
            this.selectBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.selectBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.selectBtn.Depth = 0;
            this.selectBtn.HighEmphasis = true;
            this.selectBtn.Icon = null;
            this.selectBtn.Location = new System.Drawing.Point(328, 130);
            this.selectBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.selectBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.selectBtn.Name = "selectBtn";
            this.selectBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.selectBtn.Size = new System.Drawing.Size(74, 36);
            this.selectBtn.TabIndex = 1;
            this.selectBtn.Text = "Select";
            this.selectBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.selectBtn.UseAccentColor = false;
            this.selectBtn.UseVisualStyleBackColor = true;
            this.selectBtn.Click += new System.EventHandler(this.selectBtn_Click);
            // 
            // Main
            // 
            this.ClientSize = new System.Drawing.Size(422, 232);
            this.Controls.Add(this.selectBtn);
            this.Controls.Add(this.cmbRoom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Netraya Canteen";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MaterialSkin.Controls.MaterialComboBox cmbRoom;
        private MaterialSkin.Controls.MaterialButton selectBtn;
    }
}

