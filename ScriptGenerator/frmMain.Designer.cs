using System;

namespace ScriptGenerator
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.mMenu = new System.Windows.Forms.MenuStrip();
            this.mFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mFileOpenMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.open = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.mMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mOpenDialog
            // 
            this.mOpenDialog.Filter = "MapleShark Binary Files|*.msb";
            this.mOpenDialog.Multiselect = true;
            this.mOpenDialog.ReadOnlyChecked = true;
            this.mOpenDialog.RestoreDirectory = true;
            this.mOpenDialog.Title = "Open";
            // 
            // mMenu
            // 
            this.mMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mFileMenu});
            this.mMenu.Location = new System.Drawing.Point(0, 0);
            this.mMenu.Name = "mMenu";
            this.mMenu.Size = new System.Drawing.Size(335, 24);
            this.mMenu.TabIndex = 1;
            // 
            // mFileMenu
            // 
            this.mFileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mFileOpenMenu});
            this.mFileMenu.Name = "mFileMenu";
            this.mFileMenu.Size = new System.Drawing.Size(36, 20);
            this.mFileMenu.Text = "&File";
            // 
            // mFileOpenMenu
            // 
            this.mFileOpenMenu.Name = "mFileOpenMenu";
            this.mFileOpenMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mFileOpenMenu.Size = new System.Drawing.Size(146, 22);
            this.mFileOpenMenu.Text = "&Open";
            this.mFileOpenMenu.Click += new System.EventHandler(this.mFileOpenMenu_Click);
            // 
            // open
            // 
            this.open.Location = new System.Drawing.Point(12, 27);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(242, 77);
            this.open.TabIndex = 2;
            this.open.Text = "Open MSB";
            this.open.UseVisualStyleBackColor = true;
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(258, 27);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(72, 77);
            this.exit.TabIndex = 3;
            this.exit.Text = "Exit";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 116);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.open);
            this.Controls.Add(this.mMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mMenu;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MapleEllinel | Script Generator";
            this.mMenu.ResumeLayout(false);
            this.mMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.OpenFileDialog mOpenDialog;
        private System.Windows.Forms.MenuStrip mMenu;
        private System.Windows.Forms.ToolStripMenuItem mFileMenu;
        private System.Windows.Forms.ToolStripMenuItem mFileOpenMenu;
        private System.Windows.Forms.Button open;
        private System.Windows.Forms.Button exit;
    }
}

