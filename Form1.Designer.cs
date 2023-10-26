namespace Minesweeper {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.gameButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.newButton = new System.Windows.Forms.ToolStripMenuItem();
            this.separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.beginnerButton = new System.Windows.Forms.ToolStripMenuItem();
            this.intermediateButton = new System.Windows.Forms.ToolStripMenuItem();
            this.expertButton = new System.Windows.Forms.ToolStripMenuItem();
            this.customButton = new System.Windows.Forms.ToolStripMenuItem();
            this.separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.marksButton = new System.Windows.Forms.ToolStripMenuItem();
            this.colorButton = new System.Windows.Forms.ToolStripMenuItem();
            this.soundButton = new System.Windows.Forms.ToolStripMenuItem();
            this.separator3 = new System.Windows.Forms.ToolStripSeparator();
            this.bestTimesButton = new System.Windows.Forms.ToolStripMenuItem();
            this.separator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitButton = new System.Windows.Forms.ToolStripMenuItem();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.panel = new Minesweeper.DoubleBufferedPanel();
            this.toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBar
            // 
            this.toolBar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameButton});
            this.toolBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolBar.Size = new System.Drawing.Size(800, 23);
            this.toolBar.TabIndex = 0;
            // 
            // gameButton
            // 
            this.gameButton.AutoToolTip = false;
            this.gameButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.gameButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newButton,
            this.separator1,
            this.beginnerButton,
            this.intermediateButton,
            this.expertButton,
            this.customButton,
            this.separator2,
            this.marksButton,
            this.colorButton,
            this.soundButton,
            this.separator3,
            this.bestTimesButton,
            this.separator4,
            this.exitButton});
            this.gameButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.gameButton.Name = "gameButton";
            this.gameButton.ShowDropDownArrow = false;
            this.gameButton.Size = new System.Drawing.Size(43, 20);
            this.gameButton.Text = "&Game";
            // 
            // newButton
            // 
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(180, 22);
            this.newButton.Text = "&New";
            this.newButton.Click += new System.EventHandler(this.StartNewGame);
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            this.separator1.Size = new System.Drawing.Size(177, 6);
            // 
            // beginnerButton
            // 
            this.beginnerButton.Name = "beginnerButton";
            this.beginnerButton.Size = new System.Drawing.Size(180, 22);
            this.beginnerButton.Text = "&Beginner";
            this.beginnerButton.Click += new System.EventHandler(this.StartBeginnerGame);
            // 
            // intermediateButton
            // 
            this.intermediateButton.Name = "intermediateButton";
            this.intermediateButton.Size = new System.Drawing.Size(180, 22);
            this.intermediateButton.Text = "&Intermediate";
            this.intermediateButton.Click += new System.EventHandler(this.StartIntermediateGame);
            // 
            // expertButton
            // 
            this.expertButton.Name = "expertButton";
            this.expertButton.Size = new System.Drawing.Size(180, 22);
            this.expertButton.Text = "&Expert";
            this.expertButton.Click += new System.EventHandler(this.StartExpertGame);
            // 
            // customButton
            // 
            this.customButton.Name = "customButton";
            this.customButton.Size = new System.Drawing.Size(180, 22);
            this.customButton.Text = "&Custom...";
            this.customButton.Click += new System.EventHandler(this.StartCustomGame);
            // 
            // separator2
            // 
            this.separator2.Name = "separator2";
            this.separator2.Size = new System.Drawing.Size(177, 6);
            // 
            // marksButton
            // 
            this.marksButton.Checked = true;
            this.marksButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.marksButton.Name = "marksButton";
            this.marksButton.Size = new System.Drawing.Size(180, 22);
            this.marksButton.Text = "&Marks (?)";
            this.marksButton.Click += new System.EventHandler(this.ToggleMarks);
            // 
            // colorButton
            // 
            this.colorButton.Checked = true;
            this.colorButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(180, 22);
            this.colorButton.Text = "Co&lor";
            this.colorButton.Click += new System.EventHandler(this.ToggleColor);
            // 
            // soundButton
            // 
            this.soundButton.Name = "soundButton";
            this.soundButton.Size = new System.Drawing.Size(180, 22);
            this.soundButton.Text = "&Sound";
            this.soundButton.Click += new System.EventHandler(this.ToggleSound);
            // 
            // separator3
            // 
            this.separator3.Name = "separator3";
            this.separator3.Size = new System.Drawing.Size(177, 6);
            // 
            // bestTimesButton
            // 
            this.bestTimesButton.Name = "bestTimesButton";
            this.bestTimesButton.Size = new System.Drawing.Size(180, 22);
            this.bestTimesButton.Text = "Best &Times...";
            // 
            // separator4
            // 
            this.separator4.Name = "separator4";
            this.separator4.Size = new System.Drawing.Size(177, 6);
            // 
            // exitButton
            // 
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(180, 22);
            this.exitButton.Text = "E&xit";
            this.exitButton.Click += new System.EventHandler(this.ExitButton);
            // 
            // timer1
            // 
            this.gameTimer.Interval = 1000;
            this.gameTimer.Tick += new System.EventHandler(this.timerTick);
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.Silver;
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 23);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(800, 427);
            this.panel.TabIndex = 1;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelPaint);
            this.panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GameMouseDown);
            this.panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GameMouseMove);
            this.panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GameMouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.toolBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Minesweeper";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.ToolStripDropDownButton gameButton;
        private System.Windows.Forms.ToolStripMenuItem newButton;
        private System.Windows.Forms.ToolStripSeparator separator1;
        private System.Windows.Forms.ToolStripMenuItem beginnerButton;
        private System.Windows.Forms.ToolStripMenuItem intermediateButton;
        private System.Windows.Forms.ToolStripMenuItem expertButton;
        private System.Windows.Forms.ToolStripMenuItem customButton;
        private System.Windows.Forms.ToolStripSeparator separator2;
        private System.Windows.Forms.ToolStripMenuItem marksButton;
        private System.Windows.Forms.ToolStripMenuItem colorButton;
        private System.Windows.Forms.ToolStripMenuItem soundButton;
        private System.Windows.Forms.ToolStripSeparator separator3;
        private System.Windows.Forms.ToolStripMenuItem bestTimesButton;
        private System.Windows.Forms.ToolStripSeparator separator4;
        private System.Windows.Forms.ToolStripMenuItem exitButton;
        private System.Windows.Forms.Timer gameTimer;
        private DoubleBufferedPanel panel;
    }
}

