
namespace DevScope.LogicAppConsumption.Copilot
{
    partial class EnableDisableLogicAppForm
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
            this.labelInstructions = new System.Windows.Forms.Label();
            this.comboBoxLogicApp = new System.Windows.Forms.ComboBox();
            this.radioButtonEnabled = new System.Windows.Forms.RadioButton();
            this.radioButtonDisabled = new System.Windows.Forms.RadioButton();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelInstructions
            // 
            this.labelInstructions.AutoSize = true;
            this.labelInstructions.Location = new System.Drawing.Point(15, 15);
            this.labelInstructions.Name = "labelInstructions";
            this.labelInstructions.Size = new System.Drawing.Size(53, 13);
            this.labelInstructions.TabIndex = 0;
            this.labelInstructions.Text = "Resource";
            // 
            // comboBoxLogicApp
            // 
            this.comboBoxLogicApp.FormattingEnabled = true;
            this.comboBoxLogicApp.Location = new System.Drawing.Point(74, 12);
            this.comboBoxLogicApp.Name = "comboBoxLogicApp";
            this.comboBoxLogicApp.Size = new System.Drawing.Size(573, 21);
            this.comboBoxLogicApp.TabIndex = 1;
            this.comboBoxLogicApp.SelectedIndexChanged += new System.EventHandler(this.comboBoxLogicApp_SelectedIndexChanged);
            // 
            // radioButtonEnabled
            // 
            this.radioButtonEnabled.AutoSize = true;
            this.radioButtonEnabled.Location = new System.Drawing.Point(767, 15);
            this.radioButtonEnabled.Name = "radioButtonEnabled";
            this.radioButtonEnabled.Size = new System.Drawing.Size(95, 17);
            this.radioButtonEnabled.TabIndex = 2;
            this.radioButtonEnabled.TabStop = true;
            this.radioButtonEnabled.Text = "State: Enabled";
            this.radioButtonEnabled.UseVisualStyleBackColor = true;
            this.radioButtonEnabled.CheckedChanged += new System.EventHandler(this.radioButtonEnabled_CheckedChanged);
            // 
            // radioButtonDisabled
            // 
            this.radioButtonDisabled.AutoSize = true;
            this.radioButtonDisabled.Location = new System.Drawing.Point(664, 15);
            this.radioButtonDisabled.Name = "radioButtonDisabled";
            this.radioButtonDisabled.Size = new System.Drawing.Size(97, 17);
            this.radioButtonDisabled.TabIndex = 3;
            this.radioButtonDisabled.TabStop = true;
            this.radioButtonDisabled.Text = "State: Disabled";
            this.radioButtonDisabled.UseVisualStyleBackColor = true;
            this.radioButtonDisabled.CheckedChanged += new System.EventHandler(this.radioButtonDisabled_CheckedChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(868, 12);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "Confirm";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // EnableDisableLogicAppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(955, 44);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.radioButtonDisabled);
            this.Controls.Add(this.radioButtonEnabled);
            this.Controls.Add(this.comboBoxLogicApp);
            this.Controls.Add(this.labelInstructions);
            this.MaximumSize = new System.Drawing.Size(971, 83);
            this.MinimumSize = new System.Drawing.Size(971, 83);
            this.Name = "EnableDisableLogicAppForm";
            this.Text = "Enable/Disable Logic App Initial status";
            this.Load += new System.EventHandler(this.EnableDisableLogicAppForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInstructions;
        private System.Windows.Forms.ComboBox comboBoxLogicApp;
        private System.Windows.Forms.RadioButton radioButtonEnabled;
        private System.Windows.Forms.RadioButton radioButtonDisabled;
        private System.Windows.Forms.Button buttonOK;
    }
}