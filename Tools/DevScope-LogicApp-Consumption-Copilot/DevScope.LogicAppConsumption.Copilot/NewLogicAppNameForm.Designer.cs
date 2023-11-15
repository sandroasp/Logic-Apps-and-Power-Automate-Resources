
namespace DevScope.LogicAppConsumption.Copilot
{
    partial class NewLogicAppNameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewLogicAppNameForm));
            this.richTextBoxNewLAName = new System.Windows.Forms.RichTextBox();
            this.buttonConfirmNewLAName = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBoxNewLAName
            // 
            this.richTextBoxNewLAName.Location = new System.Drawing.Point(104, 12);
            this.richTextBoxNewLAName.Name = "richTextBoxNewLAName";
            this.richTextBoxNewLAName.Size = new System.Drawing.Size(591, 21);
            this.richTextBoxNewLAName.TabIndex = 0;
            this.richTextBoxNewLAName.Text = "LA_";
            this.richTextBoxNewLAName.TextChanged += new System.EventHandler(this.richTextBoxNewLAName_TextChanged);
            // 
            // buttonConfirmNewLAName
            // 
            this.buttonConfirmNewLAName.Location = new System.Drawing.Point(701, 12);
            this.buttonConfirmNewLAName.Name = "buttonConfirmNewLAName";
            this.buttonConfirmNewLAName.Size = new System.Drawing.Size(87, 21);
            this.buttonConfirmNewLAName.TabIndex = 1;
            this.buttonConfirmNewLAName.Text = "Confirm";
            this.buttonConfirmNewLAName.UseVisualStyleBackColor = true;
            this.buttonConfirmNewLAName.Click += new System.EventHandler(this.buttonConfirmNewLAName_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Logic App Name";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // NewLogicAppNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 51);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonConfirmNewLAName);
            this.Controls.Add(this.richTextBoxNewLAName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(816, 90);
            this.MinimumSize = new System.Drawing.Size(816, 90);
            this.Name = "NewLogicAppNameForm";
            this.Text = "Set Logic App Name";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxNewLAName;
        private System.Windows.Forms.Button buttonConfirmNewLAName;
        private System.Windows.Forms.Label label1;
    }
}