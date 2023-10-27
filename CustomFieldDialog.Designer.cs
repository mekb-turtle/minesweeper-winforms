namespace Minesweeper {
    partial class CustomFieldDialog {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomFieldDialog));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.widthLabel = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.minesLabel = new System.Windows.Forms.Label();
            this.widthBox = new System.Windows.Forms.TextBox();
            this.heightBox = new System.Windows.Forms.TextBox();
            this.minesBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(119, 22);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(62, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.ClickOKButton);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(119, 71);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(62, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.ClickCancelButton);
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(13, 25);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(38, 13);
            this.widthLabel.TabIndex = 2;
            this.widthLabel.Text = "Width:";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(13, 51);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(41, 13);
            this.heightLabel.TabIndex = 3;
            this.heightLabel.Text = "Height:";
            // 
            // minesLabel
            // 
            this.minesLabel.AutoSize = true;
            this.minesLabel.Location = new System.Drawing.Point(16, 77);
            this.minesLabel.Name = "minesLabel";
            this.minesLabel.Size = new System.Drawing.Size(38, 13);
            this.minesLabel.TabIndex = 4;
            this.minesLabel.Text = "Mines:";
            // 
            // widthBox
            // 
            this.widthBox.Location = new System.Drawing.Point(57, 22);
            this.widthBox.Name = "widthBox";
            this.widthBox.Size = new System.Drawing.Size(31, 20);
            this.widthBox.TabIndex = 5;
            this.widthBox.TextChanged += new System.EventHandler(this.ValidateWidthBox);
            this.widthBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.ValidateWidthBox);
            this.widthBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ValidateWidthBox);
            this.widthBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateWidthBox);
            this.widthBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ValidateWidthBox);
            // 
            // heightBox
            // 
            this.heightBox.Location = new System.Drawing.Point(57, 48);
            this.heightBox.Name = "heightBox";
            this.heightBox.Size = new System.Drawing.Size(31, 20);
            this.heightBox.TabIndex = 6;
            this.heightBox.TextChanged += new System.EventHandler(this.ValidateHeightBox);
            this.heightBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.ValidateHeightBox);
            this.heightBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ValidateHeightBox);
            this.heightBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateHeightBox);
            this.heightBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ValidateHeightBox);
            // 
            // minesBox
            // 
            this.minesBox.Location = new System.Drawing.Point(57, 74);
            this.minesBox.Name = "minesBox";
            this.minesBox.Size = new System.Drawing.Size(31, 20);
            this.minesBox.TabIndex = 7;
            this.minesBox.TextChanged += new System.EventHandler(this.ValidateMinesBox);
            this.minesBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.ValidateMinesBox);
            this.minesBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ValidateMinesBox);
            this.minesBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateMinesBox);
            this.minesBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ValidateMinesBox);
            // 
            // CustomFieldDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(200, 114);
            this.Controls.Add(this.minesBox);
            this.Controls.Add(this.heightBox);
            this.Controls.Add(this.widthBox);
            this.Controls.Add(this.minesLabel);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.widthLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomFieldDialog";
            this.Text = "Custom Field";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.Label minesLabel;
        private System.Windows.Forms.TextBox widthBox;
        private System.Windows.Forms.TextBox heightBox;
        private System.Windows.Forms.TextBox minesBox;
    }
}