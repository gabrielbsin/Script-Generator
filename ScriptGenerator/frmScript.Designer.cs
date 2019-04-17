namespace ScriptGenerator
{
    partial class frmScript
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScript));
            this.scriptName = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.script = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // scriptName
            // 
            this.scriptName.Location = new System.Drawing.Point(74, 9);
            this.scriptName.Name = "scriptName";
            this.scriptName.ReadOnly = true;
            this.scriptName.Size = new System.Drawing.Size(171, 25);
            this.scriptName.TabIndex = 7;
            this.scriptName.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Script: ";
            // 
            // script
            // 
            this.script.Location = new System.Drawing.Point(0, 40);
            this.script.Name = "script";
            this.script.ReadOnly = true;
            this.script.Size = new System.Drawing.Size(800, 401);
            this.script.TabIndex = 5;
            this.script.Text = "";
            // 
            // frmScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.scriptName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.script);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmScript";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Script Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RichTextBox scriptName;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.RichTextBox script;
    }
}