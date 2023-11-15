
namespace DevScope.LogicAppConsumption.Copilot
{
    partial class NewEmptyLogicAppNameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewEmptyLogicAppNameForm));
            this.label1 = new System.Windows.Forms.Label();
            this.buttonConfirmNewEmptyLAName = new System.Windows.Forms.Button();
            this.richTextBoxNewEmptyLAName = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Logic App Name";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // buttonConfirmNewEmptyLAName
            // 
            this.buttonConfirmNewEmptyLAName.Location = new System.Drawing.Point(701, 12);
            this.buttonConfirmNewEmptyLAName.Name = "buttonConfirmNewEmptyLAName";
            this.buttonConfirmNewEmptyLAName.Size = new System.Drawing.Size(87, 21);
            this.buttonConfirmNewEmptyLAName.TabIndex = 9;
            this.buttonConfirmNewEmptyLAName.Text = "Confirm";
            this.buttonConfirmNewEmptyLAName.UseVisualStyleBackColor = true;
            this.buttonConfirmNewEmptyLAName.Click += new System.EventHandler(this.buttonConfirmNewEmptyLAName_Click);
            // 
            // richTextBoxNewEmptyLAName
            // 
            this.richTextBoxNewEmptyLAName.Location = new System.Drawing.Point(104, 12);
            this.richTextBoxNewEmptyLAName.Name = "richTextBoxNewEmptyLAName";
            this.richTextBoxNewEmptyLAName.Size = new System.Drawing.Size(591, 21);
            this.richTextBoxNewEmptyLAName.TabIndex = 8;
            this.richTextBoxNewEmptyLAName.Text = "";
            this.richTextBoxNewEmptyLAName.TextChanged += new System.EventHandler(this.richTextBoxNewEmptyLAName_TextChanged);
            // 
            // NewEmptyLogicAppNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 48);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonConfirmNewEmptyLAName);
            this.Controls.Add(this.richTextBoxNewEmptyLAName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(816, 87);
            this.MinimumSize = new System.Drawing.Size(816, 87);
            this.Name = "NewEmptyLogicAppNameForm";
            this.Text = "Add New Empty Logic App Template";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonConfirmNewEmptyLAName;
        private System.Windows.Forms.RichTextBox richTextBoxNewEmptyLAName;
    }
}