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
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.menuBar = new System.Windows.Forms.MainMenu(this.components);
            this.gameButton = new System.Windows.Forms.MenuItem();
            this.newButton = new System.Windows.Forms.MenuItem();
            this.separator1 = new System.Windows.Forms.MenuItem();
            this.beginnerButton = new System.Windows.Forms.MenuItem();
            this.intermediateButton = new System.Windows.Forms.MenuItem();
            this.expertButton = new System.Windows.Forms.MenuItem();
            this.customButton = new System.Windows.Forms.MenuItem();
            this.separator2 = new System.Windows.Forms.MenuItem();
            this.marksButton = new System.Windows.Forms.MenuItem();
            this.colorButton = new System.Windows.Forms.MenuItem();
            this.soundButton = new System.Windows.Forms.MenuItem();
            this.separator3 = new System.Windows.Forms.MenuItem();
            this.saveButton = new System.Windows.Forms.MenuItem();
            this.separator4 = new System.Windows.Forms.MenuItem();
            this.exitButton = new System.Windows.Forms.MenuItem();
            this.windowButton = new System.Windows.Forms.MenuItem();
            this.scale1Button = new System.Windows.Forms.MenuItem();
            this.scale2Button = new System.Windows.Forms.MenuItem();
            this.scale3Button = new System.Windows.Forms.MenuItem();
            this.scale4Button = new System.Windows.Forms.MenuItem();
            this.separator5 = new System.Windows.Forms.MenuItem();
            this.fullscreenButton = new System.Windows.Forms.MenuItem();
            this.solverButton = new System.Windows.Forms.MenuItem();
            this.makeOneMoveButton = new System.Windows.Forms.MenuItem();
            this.solveButton = new System.Windows.Forms.MenuItem();
            this.autoSolveButton = new System.Windows.Forms.MenuItem();
            this.modesButton = new System.Windows.Forms.MenuItem();
            this.firstMoveClearButton = new System.Windows.Forms.MenuItem();
            this.logicButton = new System.Windows.Forms.MenuItem();
            this.panel = new Minesweeper.DoubleBufferedPanel();
            this.SuspendLayout();
            // 
            // gameTimer
            // 
            this.gameTimer.Tick += new System.EventHandler(this.TimerUpdateTick);
            // 
            // menuBar
            // 
            this.menuBar.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.gameButton,
            this.windowButton,
            this.solverButton,
            this.modesButton});
            // 
            // gameButton
            // 
            this.gameButton.Index = 0;
            this.gameButton.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
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
            this.saveButton,
            this.separator4,
            this.exitButton});
            this.gameButton.Text = "&Game";
            // 
            // newButton
            // 
            this.newButton.Index = 0;
            this.newButton.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.newButton.Text = "&New";
            this.newButton.Click += new System.EventHandler(this.StartNewGame);
            // 
            // separator1
            // 
            this.separator1.Index = 1;
            this.separator1.Text = "-";
            // 
            // beginnerButton
            // 
            this.beginnerButton.Index = 2;
            this.beginnerButton.Text = "&Beginner";
            this.beginnerButton.Click += new System.EventHandler(this.StartBeginnerGame);
            // 
            // intermediateButton
            // 
            this.intermediateButton.Index = 3;
            this.intermediateButton.Text = "&Intermediate";
            this.intermediateButton.Click += new System.EventHandler(this.StartIntermediateGame);
            // 
            // expertButton
            // 
            this.expertButton.Index = 4;
            this.expertButton.Text = "&Expert";
            this.expertButton.Click += new System.EventHandler(this.StartExpertGame);
            // 
            // customButton
            // 
            this.customButton.Index = 5;
            this.customButton.Text = "&Custom...";
            this.customButton.Click += new System.EventHandler(this.StartCustomGame);
            // 
            // separator2
            // 
            this.separator2.Index = 6;
            this.separator2.Text = "-";
            // 
            // marksButton
            // 
            this.marksButton.Index = 7;
            this.marksButton.Text = "&Marks (?)";
            this.marksButton.Click += new System.EventHandler(this.ToggleMarks);
            // 
            // colorButton
            // 
            this.colorButton.Index = 8;
            this.colorButton.Text = "Co&lor";
            this.colorButton.Click += new System.EventHandler(this.ToggleColor);
            // 
            // soundButton
            // 
            this.soundButton.Index = 9;
            this.soundButton.Text = "&Sound";
            this.soundButton.Click += new System.EventHandler(this.ToggleSound);
            // 
            // separator3
            // 
            this.separator3.Index = 10;
            this.separator3.Text = "-";
            // 
            // saveButton
            // 
            this.saveButton.Index = 11;
            this.saveButton.Text = "Sa&ve Screenshot...";
            this.saveButton.Click += new System.EventHandler(this.SaveImage);
            // 
            // separator4
            // 
            this.separator4.Index = 12;
            this.separator4.Text = "-";
            // 
            // exitButton
            // 
            this.exitButton.Index = 13;
            this.exitButton.Text = "E&xit";
            this.exitButton.Click += new System.EventHandler(this.ExitButtonClick);
            // 
            // windowButton
            // 
            this.windowButton.Index = 1;
            this.windowButton.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.scale1Button,
            this.scale2Button,
            this.scale3Button,
            this.scale4Button,
            this.separator5,
            this.fullscreenButton});
            this.windowButton.Text = "&Window";
            // 
            // scale1Button
            // 
            this.scale1Button.Index = 0;
            this.scale1Button.Text = "&1x Scale";
            this.scale1Button.Click += new System.EventHandler(this.ScaleWindow1);
            // 
            // scale2Button
            // 
            this.scale2Button.Index = 1;
            this.scale2Button.Text = "&2x Scale";
            this.scale2Button.Click += new System.EventHandler(this.ScaleWindow2);
            // 
            // scale3Button
            // 
            this.scale3Button.Index = 2;
            this.scale3Button.Text = "&3x Scale";
            this.scale3Button.Click += new System.EventHandler(this.ScaleWindow3);
            // 
            // scale4Button
            // 
            this.scale4Button.Index = 3;
            this.scale4Button.Text = "&4x Scale";
            this.scale4Button.Click += new System.EventHandler(this.ScaleWindow4);
            // 
            // separator5
            // 
            this.separator5.Index = 4;
            this.separator5.Text = "-";
            // 
            // fullscreenButton
            // 
            this.fullscreenButton.Index = 5;
            this.fullscreenButton.Shortcut = System.Windows.Forms.Shortcut.F11;
            this.fullscreenButton.Text = "&Fullscreen";
            this.fullscreenButton.Click += new System.EventHandler(this.ToggleFullscreen);
            // 
            // solverButton
            // 
            this.solverButton.Index = 2;
            this.solverButton.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.makeOneMoveButton,
            this.solveButton,
            this.autoSolveButton});
            this.solverButton.Text = "&Solver";
            // 
            // makeOneMoveButton
            // 
            this.makeOneMoveButton.Index = 0;
            this.makeOneMoveButton.Shortcut = System.Windows.Forms.Shortcut.F7;
            this.makeOneMoveButton.Text = "Make &One Move";
            this.makeOneMoveButton.Click += new System.EventHandler(this.MakeOneMove);
            // 
            // solveButton
            // 
            this.solveButton.Index = 1;
            this.solveButton.Shortcut = System.Windows.Forms.Shortcut.F8;
            this.solveButton.Text = "&Solve";
            this.solveButton.Click += new System.EventHandler(this.MakeMultipleMoves);
            // 
            // autoSolveButton
            // 
            this.autoSolveButton.Index = 2;
            this.autoSolveButton.Text = "&Auto-Solve";
            this.autoSolveButton.Click += new System.EventHandler(this.ToggleAutoSolve);
            // 
            // modesButton
            // 
            this.modesButton.Index = 3;
            this.modesButton.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.firstMoveClearButton,
            this.logicButton});
            this.modesButton.Text = "Modes";
            // 
            // firstMoveClearButton
            // 
            this.firstMoveClearButton.Index = 0;
            this.firstMoveClearButton.Text = "Blank First Move";
            this.firstMoveClearButton.Click += new System.EventHandler(this.ToggleFirstMoveClear);
            // 
            // logicButton
            // 
            this.logicButton.Index = 1;
            this.logicButton.Text = "Logic / No Guessing";
            this.logicButton.Click += new System.EventHandler(this.ToggleLogic);
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.Black;
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(348, 359);
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
            this.ClientSize = new System.Drawing.Size(348, 359);
            this.Controls.Add(this.panel);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.menuBar;
            this.Name = "Form1";
            this.Text = "Minesweeper";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.FormSizeChanged);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer gameTimer;
        private DoubleBufferedPanel panel;
        private System.Windows.Forms.MainMenu menuBar;
        private System.Windows.Forms.MenuItem gameButton;
        private System.Windows.Forms.MenuItem newButton;
        private System.Windows.Forms.MenuItem separator1;
        private System.Windows.Forms.MenuItem beginnerButton;
        private System.Windows.Forms.MenuItem intermediateButton;
        private System.Windows.Forms.MenuItem expertButton;
        private System.Windows.Forms.MenuItem customButton;
        private System.Windows.Forms.MenuItem separator2;
        private System.Windows.Forms.MenuItem marksButton;
        private System.Windows.Forms.MenuItem colorButton;
        private System.Windows.Forms.MenuItem soundButton;
        private System.Windows.Forms.MenuItem separator3;
        private System.Windows.Forms.MenuItem saveButton;
        private System.Windows.Forms.MenuItem exitButton;
        private System.Windows.Forms.MenuItem windowButton;
        private System.Windows.Forms.MenuItem scale1Button;
        private System.Windows.Forms.MenuItem scale2Button;
        private System.Windows.Forms.MenuItem scale3Button;
        private System.Windows.Forms.MenuItem scale4Button;
        private System.Windows.Forms.MenuItem solverButton;
        private System.Windows.Forms.MenuItem firstMoveClearButton;
        private System.Windows.Forms.MenuItem separator4;
        private System.Windows.Forms.MenuItem autoSolveButton;
        private System.Windows.Forms.MenuItem makeOneMoveButton;
        private System.Windows.Forms.MenuItem solveButton;
        private System.Windows.Forms.MenuItem modesButton;
        private System.Windows.Forms.MenuItem logicButton;
        private System.Windows.Forms.MenuItem separator5;
        private System.Windows.Forms.MenuItem fullscreenButton;
    }
}

