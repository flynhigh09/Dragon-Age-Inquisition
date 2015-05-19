namespace Dragon_Age_Inquisition
{
    using System.Security.AccessControl;

    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.InfHealth = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.InfItems = new System.Windows.Forms.Button();
            this.InquisitionEXPmultiplier = new System.Windows.Forms.Button();
            this.EXPMultiplier = new System.Windows.Forms.Button();
            this.NoCooldown = new System.Windows.Forms.Button();
            this.Infiniteabilitypoints = new System.Windows.Forms.Button();
            this.ScreenTimer = new System.Windows.Forms.Timer(this.components);
            this.Update = new System.Windows.Forms.NumericUpDown();
            this.AddressTimer = new System.Windows.Forms.Timer(this.components);
            this.PROG = new System.Windows.Forms.ProgressBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.Connect = new System.Windows.Forms.ToolStripMenuItem();
            this.Options = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadAll = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.Game = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Attached = new System.Windows.Forms.ToolStripLabel();
            this.SetAll = new System.Windows.Forms.Button();
            this.Output = new System.Windows.Forms.RichTextBox();
            this.Infmana = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Update)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // InfHealth
            // 
            this.InfHealth.BackColor = System.Drawing.Color.Black;
            this.InfHealth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InfHealth.ForeColor = System.Drawing.Color.YellowGreen;
            this.InfHealth.Location = new System.Drawing.Point(9, 182);
            this.InfHealth.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.InfHealth.Name = "InfHealth";
            this.InfHealth.Size = new System.Drawing.Size(137, 53);
            this.InfHealth.TabIndex = 19;
            this.InfHealth.Text = "Inf Health";
            this.InfHealth.UseVisualStyleBackColor = false;
            this.InfHealth.Click += new System.EventHandler(this.InfHealth_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Dragon_Age_Inquisition.Properties.Resources.Da;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(118, 26);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(422, 142);
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // InfItems
            // 
            this.InfItems.BackColor = System.Drawing.Color.Black;
            this.InfItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InfItems.ForeColor = System.Drawing.Color.YellowGreen;
            this.InfItems.Location = new System.Drawing.Point(9, 248);
            this.InfItems.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.InfItems.Name = "InfItems";
            this.InfItems.Size = new System.Drawing.Size(137, 53);
            this.InfItems.TabIndex = 18;
            this.InfItems.Text = "Inf Items";
            this.InfItems.UseVisualStyleBackColor = false;
            this.InfItems.Click += new System.EventHandler(this.InfItems_Click);
            // 
            // InquisitionEXPmultiplier
            // 
            this.InquisitionEXPmultiplier.BackColor = System.Drawing.Color.Black;
            this.InquisitionEXPmultiplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InquisitionEXPmultiplier.ForeColor = System.Drawing.Color.YellowGreen;
            this.InquisitionEXPmultiplier.Location = new System.Drawing.Point(154, 308);
            this.InquisitionEXPmultiplier.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.InquisitionEXPmultiplier.Name = "InquisitionEXPmultiplier";
            this.InquisitionEXPmultiplier.Size = new System.Drawing.Size(128, 53);
            this.InquisitionEXPmultiplier.TabIndex = 23;
            this.InquisitionEXPmultiplier.Text = "Inquisition Exp Multiplier";
            this.InquisitionEXPmultiplier.UseVisualStyleBackColor = false;
            this.InquisitionEXPmultiplier.Click += new System.EventHandler(this.InquisitionEXPmultiplier_Click);
            // 
            // EXPMultiplier
            // 
            this.EXPMultiplier.BackColor = System.Drawing.Color.Black;
            this.EXPMultiplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EXPMultiplier.ForeColor = System.Drawing.Color.YellowGreen;
            this.EXPMultiplier.Location = new System.Drawing.Point(154, 248);
            this.EXPMultiplier.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.EXPMultiplier.Name = "EXPMultiplier";
            this.EXPMultiplier.Size = new System.Drawing.Size(128, 53);
            this.EXPMultiplier.TabIndex = 21;
            this.EXPMultiplier.Text = "EXP Multiplier";
            this.EXPMultiplier.UseVisualStyleBackColor = false;
            this.EXPMultiplier.Click += new System.EventHandler(this.EXPMultiplier_Click);
            // 
            // NoCooldown
            // 
            this.NoCooldown.BackColor = System.Drawing.Color.Black;
            this.NoCooldown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NoCooldown.ForeColor = System.Drawing.Color.YellowGreen;
            this.NoCooldown.Location = new System.Drawing.Point(154, 182);
            this.NoCooldown.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.NoCooldown.Name = "NoCooldown";
            this.NoCooldown.Size = new System.Drawing.Size(128, 53);
            this.NoCooldown.TabIndex = 22;
            this.NoCooldown.Text = "No Cooldown";
            this.NoCooldown.UseVisualStyleBackColor = false;
            this.NoCooldown.Click += new System.EventHandler(this.NoCooldown_Click);
            // 
            // Infiniteabilitypoints
            // 
            this.Infiniteabilitypoints.BackColor = System.Drawing.Color.Black;
            this.Infiniteabilitypoints.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Infiniteabilitypoints.ForeColor = System.Drawing.Color.YellowGreen;
            this.Infiniteabilitypoints.Location = new System.Drawing.Point(9, 308);
            this.Infiniteabilitypoints.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.Infiniteabilitypoints.Name = "Infiniteabilitypoints";
            this.Infiniteabilitypoints.Size = new System.Drawing.Size(137, 53);
            this.Infiniteabilitypoints.TabIndex = 20;
            this.Infiniteabilitypoints.Text = "Infinite Ability Points";
            this.Infiniteabilitypoints.UseVisualStyleBackColor = false;
            this.Infiniteabilitypoints.Click += new System.EventHandler(this.Infiniteabilitypoints_Click);
            // 
            // ScreenTimer
            // 
            this.ScreenTimer.Tick += new System.EventHandler(this.ScreenTimer_Tick);
            // 
            // Update
            // 
            this.Update.DecimalPlaces = 2;
            this.Update.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.Update.Location = new System.Drawing.Point(9, 29);
            this.Update.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Update.Name = "Update";
            this.Update.Size = new System.Drawing.Size(66, 29);
            this.Update.TabIndex = 22;
            this.Update.Value = new decimal(new int[] {
            101,
            0,
            0,
            131072});
            // 
            // AddressTimer
            // 
            this.AddressTimer.Tick += new System.EventHandler(this.AddressTimer_Tick);
            // 
            // PROG
            // 
            this.PROG.Location = new System.Drawing.Point(7, 364);
            this.PROG.Maximum = 60;
            this.PROG.Name = "PROG";
            this.PROG.Size = new System.Drawing.Size(533, 19);
            this.PROG.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.PROG.TabIndex = 25;
            this.PROG.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.Game,
            this.toolStripSeparator1,
            this.Attached});
            this.toolStrip1.Location = new System.Drawing.Point(0, -1);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(115, 27);
            this.toolStrip1.TabIndex = 26;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Connect,
            this.Options,
            this.LoadAll,
            this.SaveAll});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(97, 24);
            this.toolStripDropDownButton1.Text = "Connection";
            // 
            // Connect
            // 
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(133, 24);
            this.Connect.Text = "Connect";
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // Options
            // 
            this.Options.Name = "Options";
            this.Options.Size = new System.Drawing.Size(133, 24);
            this.Options.Text = "Options";
            this.Options.Click += new System.EventHandler(this.Options_Click);
            // 
            // LoadAll
            // 
            this.LoadAll.Name = "LoadAll";
            this.LoadAll.Size = new System.Drawing.Size(133, 24);
            this.LoadAll.Text = "Load All";
            this.LoadAll.Click += new System.EventHandler(this.LoadAll_Click);
            // 
            // SaveAll
            // 
            this.SaveAll.Name = "SaveAll";
            this.SaveAll.Size = new System.Drawing.Size(133, 24);
            this.SaveAll.Text = "Save All";
            this.SaveAll.Click += new System.EventHandler(this.SaveAll_Click);
            // 
            // Game
            // 
            this.Game.Name = "Game";
            this.Game.Size = new System.Drawing.Size(0, 24);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // Attached
            // 
            this.Attached.Name = "Attached";
            this.Attached.Size = new System.Drawing.Size(0, 24);
            // 
            // SetAll
            // 
            this.SetAll.BackColor = System.Drawing.Color.Black;
            this.SetAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SetAll.ForeColor = System.Drawing.Color.YellowGreen;
            this.SetAll.Location = new System.Drawing.Point(9, 113);
            this.SetAll.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.SetAll.Name = "SetAll";
            this.SetAll.Size = new System.Drawing.Size(102, 53);
            this.SetAll.TabIndex = 27;
            this.SetAll.Text = "Set All";
            this.SetAll.UseVisualStyleBackColor = false;
            this.SetAll.Click += new System.EventHandler(this.SetAll_Click);
            // 
            // Output
            // 
            this.Output.Location = new System.Drawing.Point(289, 168);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(251, 193);
            this.Output.TabIndex = 28;
            this.Output.Text = "";
            // 
            // Infmana
            // 
            this.Infmana.AutoSize = true;
            this.Infmana.Location = new System.Drawing.Point(22, 77);
            this.Infmana.Name = "Infmana";
            this.Infmana.Size = new System.Drawing.Size(90, 25);
            this.Infmana.TabIndex = 29;
            this.Infmana.Text = "Inf mana";
            this.Infmana.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(550, 387);
            this.Controls.Add(this.Infmana);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.SetAll);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.PROG);
            this.Controls.Add(this.NoCooldown);
            this.Controls.Add(this.InquisitionEXPmultiplier);
            this.Controls.Add(this.EXPMultiplier);
            this.Controls.Add(this.Infiniteabilitypoints);
            this.Controls.Add(this.Update);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.InfItems);
            this.Controls.Add(this.InfHealth);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dragon Age Inquisition All Updates";
            this.Closed += new System.EventHandler(this.Form1_Closing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Update)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button InfItems;
        private System.Windows.Forms.Timer ScreenTimer;
        private System.Windows.Forms.Button InfHealth;
        private System.Windows.Forms.Button EXPMultiplier;
        private System.Windows.Forms.Button Infiniteabilitypoints;
        private System.Windows.Forms.Button InquisitionEXPmultiplier;
        private System.Windows.Forms.Button NoCooldown;
        private System.Windows.Forms.NumericUpDown Update;
        private System.Windows.Forms.Timer AddressTimer;
        private System.Windows.Forms.ProgressBar PROG;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem LoadAll;
        private System.Windows.Forms.ToolStripMenuItem SaveAll;
        private System.Windows.Forms.ToolStripMenuItem Connect;
        private System.Windows.Forms.ToolStripMenuItem Options;
        private System.Windows.Forms.ToolStripLabel Game;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel Attached;
        private System.Windows.Forms.Button SetAll;
        private System.Windows.Forms.RichTextBox Output;
        private System.Windows.Forms.CheckBox Infmana;
    }
}

