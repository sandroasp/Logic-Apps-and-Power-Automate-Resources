
namespace DevScope.LogicAppConsumption.Copilot
{
    partial class AddWorkFlowParameters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddWorkFlowParameters));
            this.richTextBoxNameWP = new System.Windows.Forms.RichTextBox();
            this.richTextBoxValueWP = new System.Windows.Forms.RichTextBox();
            this.SubmitButtonWP = new System.Windows.Forms.Button();
            this.comboBoxTypeWP = new System.Windows.Forms.ComboBox();
            this.labelVarName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxresourceNumberWP = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // richTextBoxNameWP
            // 
            this.richTextBoxNameWP.Location = new System.Drawing.Point(108, 39);
            this.richTextBoxNameWP.Name = "richTextBoxNameWP";
            this.richTextBoxNameWP.Size = new System.Drawing.Size(293, 21);
            this.richTextBoxNameWP.TabIndex = 0;
            this.richTextBoxNameWP.Text = "p_";
            this.richTextBoxNameWP.TextChanged += new System.EventHandler(this.richTextBoxNameWP_TextChanged);
            // 
            // richTextBoxValueWP
            // 
            this.richTextBoxValueWP.Location = new System.Drawing.Point(108, 93);
            this.richTextBoxValueWP.Name = "richTextBoxValueWP";
            this.richTextBoxValueWP.Size = new System.Drawing.Size(293, 21);
            this.richTextBoxValueWP.TabIndex = 1;
            this.richTextBoxValueWP.Text = "";
            this.richTextBoxValueWP.TextChanged += new System.EventHandler(this.richTextBoxValueWP_TextChanged);
            // 
            // SubmitButtonWP
            // 
            this.SubmitButtonWP.Location = new System.Drawing.Point(407, 12);
            this.SubmitButtonWP.Name = "SubmitButtonWP";
            this.SubmitButtonWP.Size = new System.Drawing.Size(132, 102);
            this.SubmitButtonWP.TabIndex = 3;
            this.SubmitButtonWP.Text = "Add Workflow Parameter";
            this.SubmitButtonWP.UseVisualStyleBackColor = true;
            this.SubmitButtonWP.Click += new System.EventHandler(this.SubmitButtonWP_Click);
            // 
            // comboBoxTypeWP
            // 
            this.comboBoxTypeWP.FormattingEnabled = true;
            this.comboBoxTypeWP.Items.AddRange(new object[] {
            "Array",
            "Bool",
            "Float",
            "Int",
            "Object",
            "Secure Object",
            "Secure String",
            "String"});
            this.comboBoxTypeWP.Location = new System.Drawing.Point(108, 66);
            this.comboBoxTypeWP.Name = "comboBoxTypeWP";
            this.comboBoxTypeWP.Size = new System.Drawing.Size(293, 21);
            this.comboBoxTypeWP.TabIndex = 4;
            this.comboBoxTypeWP.Text = "String";
            this.comboBoxTypeWP.SelectedIndexChanged += new System.EventHandler(this.comboBoxTypeWP_SelectedIndexChanged);
            // 
            // labelVarName
            // 
            this.labelVarName.AutoSize = true;
            this.labelVarName.Location = new System.Drawing.Point(67, 39);
            this.labelVarName.Name = "labelVarName";
            this.labelVarName.Size = new System.Drawing.Size(35, 13);
            this.labelVarName.TabIndex = 7;
            this.labelVarName.Text = "Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Value";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Resource Name";
            // 
            // comboBoxresourceNumberWP
            // 
            this.comboBoxresourceNumberWP.FormattingEnabled = true;
            this.comboBoxresourceNumberWP.Location = new System.Drawing.Point(108, 12);
            this.comboBoxresourceNumberWP.Name = "comboBoxresourceNumberWP";
            this.comboBoxresourceNumberWP.Size = new System.Drawing.Size(293, 21);
            this.comboBoxresourceNumberWP.TabIndex = 11;
            this.comboBoxresourceNumberWP.SelectedIndexChanged += new System.EventHandler(this.comboBoxresourceNumberWP_SelectedIndexChanged);
            // 
            // AddWorkFlowParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(551, 124);
            this.Controls.Add(this.comboBoxresourceNumberWP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelVarName);
            this.Controls.Add(this.comboBoxTypeWP);
            this.Controls.Add(this.SubmitButtonWP);
            this.Controls.Add(this.richTextBoxValueWP);
            this.Controls.Add(this.richTextBoxNameWP);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(567, 163);
            this.MinimumSize = new System.Drawing.Size(567, 163);
            this.Name = "AddWorkFlowParameters";
            this.Text = "Add Logic App Parameter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxNameWP;
        private System.Windows.Forms.RichTextBox richTextBoxValueWP;
        private System.Windows.Forms.Button SubmitButtonWP;
        private System.Windows.Forms.ComboBox comboBoxTypeWP;
        private System.Windows.Forms.Label labelVarName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxresourceNumberWP;
    }
}