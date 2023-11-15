
namespace DevScope.LogicAppConsumption.Copilot
{
    partial class AddVariablesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddVariablesForm));
            this.richTextBoxInitVariableName = new System.Windows.Forms.RichTextBox();
            this.richTextBoxInitVariableValue = new System.Windows.Forms.RichTextBox();
            this.AddVariableButton = new System.Windows.Forms.Button();
            this.labelVarName = new System.Windows.Forms.Label();
            this.labelVarValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBoxInitVariableName
            // 
            this.richTextBoxInitVariableName.Location = new System.Drawing.Point(54, 12);
            this.richTextBoxInitVariableName.Name = "richTextBoxInitVariableName";
            this.richTextBoxInitVariableName.Size = new System.Drawing.Size(309, 21);
            this.richTextBoxInitVariableName.TabIndex = 0;
            this.richTextBoxInitVariableName.Text = "";
            // 
            // richTextBoxInitVariableValue
            // 
            this.richTextBoxInitVariableValue.Location = new System.Drawing.Point(54, 39);
            this.richTextBoxInitVariableValue.Name = "richTextBoxInitVariableValue";
            this.richTextBoxInitVariableValue.Size = new System.Drawing.Size(309, 21);
            this.richTextBoxInitVariableValue.TabIndex = 1;
            this.richTextBoxInitVariableValue.Text = "";
            // 
            // AddVariableButton
            // 
            this.AddVariableButton.Location = new System.Drawing.Point(369, 12);
            this.AddVariableButton.Name = "AddVariableButton";
            this.AddVariableButton.Size = new System.Drawing.Size(75, 50);
            this.AddVariableButton.TabIndex = 3;
            this.AddVariableButton.Text = "AddVariable";
            this.AddVariableButton.UseVisualStyleBackColor = true;
            this.AddVariableButton.Click += new System.EventHandler(this.AddVariableButton_Click);
            // 
            // labelVarName
            // 
            this.labelVarName.AutoSize = true;
            this.labelVarName.Location = new System.Drawing.Point(13, 12);
            this.labelVarName.Name = "labelVarName";
            this.labelVarName.Size = new System.Drawing.Size(35, 13);
            this.labelVarName.TabIndex = 4;
            this.labelVarName.Text = "Name";
            // 
            // labelVarValue
            // 
            this.labelVarValue.AutoSize = true;
            this.labelVarValue.Location = new System.Drawing.Point(13, 39);
            this.labelVarValue.Name = "labelVarValue";
            this.labelVarValue.Size = new System.Drawing.Size(34, 13);
            this.labelVarValue.TabIndex = 5;
            this.labelVarValue.Text = "Value";
            // 
            // AddVariablesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(456, 74);
            this.Controls.Add(this.labelVarValue);
            this.Controls.Add(this.labelVarName);
            this.Controls.Add(this.AddVariableButton);
            this.Controls.Add(this.richTextBoxInitVariableValue);
            this.Controls.Add(this.richTextBoxInitVariableName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(472, 113);
            this.MinimumSize = new System.Drawing.Size(472, 113);
            this.Name = "AddVariablesForm";
            this.Text = "Add Variables";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxInitVariableName;
        private System.Windows.Forms.RichTextBox richTextBoxInitVariableValue;
        private System.Windows.Forms.Button AddVariableButton;
        private System.Windows.Forms.Label labelVarName;
        private System.Windows.Forms.Label labelVarValue;
    }
}