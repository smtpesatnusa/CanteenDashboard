namespace NetrayaDashboard
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
            this.SuspendLayout();
            // 
            // cmbRoom
            // 
            this.cmbRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRoom.AutoResize = false;
            this.cmbRoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbRoom.Depth = 0;
            this.cmbRoom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbRoom.DropDownHeight = 174;
            this.cmbRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoom.DropDownWidth = 121;
            this.cmbRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbRoom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbRoom.FormattingEnabled = true;
            this.cmbRoom.Hint = "Select Room";
            this.cmbRoom.IntegralHeight = false;
            this.cmbRoom.ItemHeight = 43;
            this.cmbRoom.Items.AddRange(new object[] {
            "SMT-MAINROOM",
            "SMT-SA",
            "SMT-DIPPING",
            "SMT-OUT"});
            this.cmbRoom.Location = new System.Drawing.Point(94, 124);
            this.cmbRoom.MaxDropDownItems = 4;
            this.cmbRoom.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbRoom.Name = "cmbRoom";
            this.cmbRoom.Size = new System.Drawing.Size(303, 49);
            this.cmbRoom.StartIndex = 0;
            this.cmbRoom.TabIndex = 0;
            this.cmbRoom.SelectedIndexChanged += new System.EventHandler(this.cmbSize_SelectedIndexChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 267);
            this.Controls.Add(this.cmbRoom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Netraya Attendance";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialComboBox cmbRoom;
    }
}

