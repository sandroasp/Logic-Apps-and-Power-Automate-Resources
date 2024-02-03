namespace DVS_LogicAppBulkFailedRunsResubmit_POC
{
    partial class BulkFailedRunsResubmit
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BulkFailedRunsResubmit));
            subscriptionRichTextBox = new RichTextBox();
            resourceGroupRichTextBox = new RichTextBox();
            logicAppRichTextBox = new RichTextBox();
            logRichTextBox = new RichTextBox();
            listView1 = new ListView();
            retrieveFailedRunsButton = new Button();
            resubmitButton = new Button();
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label8 = new Label();
            progressLabel = new Label();
            menuStrip1 = new MenuStrip();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            runCounter = new Label();
            dateTimePickerEarliest = new DateTimePicker();
            dateTimePickerLatest = new DateTimePicker();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // subscriptionRichTextBox
            // 
            subscriptionRichTextBox.Location = new Point(114, 93);
            subscriptionRichTextBox.Name = "subscriptionRichTextBox";
            subscriptionRichTextBox.Size = new Size(674, 26);
            subscriptionRichTextBox.TabIndex = 0;
            subscriptionRichTextBox.Text = "";
            // 
            // resourceGroupRichTextBox
            // 
            resourceGroupRichTextBox.Location = new Point(114, 62);
            resourceGroupRichTextBox.Name = "resourceGroupRichTextBox";
            resourceGroupRichTextBox.Size = new Size(674, 26);
            resourceGroupRichTextBox.TabIndex = 1;
            resourceGroupRichTextBox.Text = "";
            // 
            // logicAppRichTextBox
            // 
            logicAppRichTextBox.Location = new Point(114, 30);
            logicAppRichTextBox.Name = "logicAppRichTextBox";
            logicAppRichTextBox.Size = new Size(674, 26);
            logicAppRichTextBox.TabIndex = 2;
            logicAppRichTextBox.Text = "";
            // 
            // logRichTextBox
            // 
            logRichTextBox.Location = new Point(458, 256);
            logRichTextBox.Name = "logRichTextBox";
            logRichTextBox.Size = new Size(330, 326);
            logRichTextBox.TabIndex = 6;
            logRichTextBox.Text = "";
            // 
            // listView1
            // 
            listView1.Location = new Point(114, 256);
            listView1.Name = "listView1";
            listView1.Size = new Size(338, 326);
            listView1.TabIndex = 7;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // retrieveFailedRunsButton
            // 
            retrieveFailedRunsButton.BackColor = SystemColors.ButtonFace;
            retrieveFailedRunsButton.Location = new Point(114, 211);
            retrieveFailedRunsButton.Name = "retrieveFailedRunsButton";
            retrieveFailedRunsButton.Size = new Size(167, 39);
            retrieveFailedRunsButton.TabIndex = 8;
            retrieveFailedRunsButton.Text = "Search Runs";
            retrieveFailedRunsButton.UseVisualStyleBackColor = false;
            retrieveFailedRunsButton.Click += retrieveFailedRunsButton_Click;
            // 
            // resubmitButton
            // 
            resubmitButton.BackColor = SystemColors.ButtonFace;
            resubmitButton.Location = new Point(285, 211);
            resubmitButton.Name = "resubmitButton";
            resubmitButton.Size = new Size(167, 39);
            resubmitButton.TabIndex = 9;
            resubmitButton.Text = "Resubmit Runs";
            resubmitButton.UseVisualStyleBackColor = false;
            resubmitButton.Click += resubmitButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.AliceBlue;
            label1.Location = new Point(7, 96);
            label1.Name = "label1";
            label1.Size = new Size(76, 15);
            label1.TabIndex = 10;
            label1.Text = "Subscription:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.AliceBlue;
            label2.Location = new Point(7, 65);
            label2.Name = "label2";
            label2.Size = new Size(94, 15);
            label2.TabIndex = 11;
            label2.Text = "Resource Group:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.AliceBlue;
            label4.Location = new Point(7, 33);
            label4.Name = "label4";
            label4.Size = new Size(99, 15);
            label4.TabIndex = 12;
            label4.Text = "Logic App Name:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.AliceBlue;
            label5.Location = new Point(7, 129);
            label5.Name = "label5";
            label5.Size = new Size(105, 15);
            label5.TabIndex = 14;
            label5.Text = "Earliest Date/Time:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(325, 129);
            label6.Name = "label6";
            label6.Size = new Size(99, 15);
            label6.TabIndex = 15;
            label6.Text = "Latest Date/Time:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 6.75F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(114, 161);
            label8.Name = "label8";
            label8.Size = new Size(347, 36);
            label8.TabIndex = 17;
            label8.Text = resources.GetString("label8.Text");
            // 
            // progressLabel
            // 
            progressLabel.AutoSize = true;
            progressLabel.Location = new Point(458, 235);
            progressLabel.Name = "progressLabel";
            progressLabel.Size = new Size(16, 15);
            progressLabel.TabIndex = 18;
            progressLabel.Text = "...";
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.AliceBlue;
            menuStrip1.Items.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 19;
            menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.BackColor = SystemColors.GradientInactiveCaption;
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(52, 20);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click_1;
            // 
            // runCounter
            // 
            runCounter.AutoSize = true;
            runCounter.Location = new Point(622, 235);
            runCounter.Name = "runCounter";
            runCounter.Size = new Size(12, 15);
            runCounter.TabIndex = 20;
            runCounter.Text = "_";
            // 
            // dateTimePickerEarliest
            // 
            dateTimePickerEarliest.CustomFormat = "yyyy-MM-dd hh:mm:ss tt";
            dateTimePickerEarliest.Format = DateTimePickerFormat.Custom;
            dateTimePickerEarliest.Location = new Point(114, 125);
            dateTimePickerEarliest.Name = "dateTimePickerEarliest";
            dateTimePickerEarliest.RightToLeft = RightToLeft.Yes;
            dateTimePickerEarliest.ShowCheckBox = true;
            dateTimePickerEarliest.ShowUpDown = true;
            dateTimePickerEarliest.Size = new Size(200, 23);
            dateTimePickerEarliest.TabIndex = 21;
            dateTimePickerEarliest.Value = new DateTime(2024, 1, 31, 0, 0, 0, 0);
            // 
            // dateTimePickerLatest
            // 
            dateTimePickerLatest.CustomFormat = "yyyy-MM-dd hh:mm:ss tt";
            dateTimePickerLatest.Format = DateTimePickerFormat.Custom;
            dateTimePickerLatest.Location = new Point(434, 125);
            dateTimePickerLatest.Name = "dateTimePickerLatest";
            dateTimePickerLatest.RightToLeftLayout = true;
            dateTimePickerLatest.ShowCheckBox = true;
            dateTimePickerLatest.ShowUpDown = true;
            dateTimePickerLatest.Size = new Size(200, 23);
            dateTimePickerLatest.TabIndex = 22;
            dateTimePickerLatest.Value = new DateTime(2024, 1, 31, 0, 0, 0, 0);
            // 
            // BulkFailedRunsResubmit
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(800, 594);
            Controls.Add(dateTimePickerLatest);
            Controls.Add(dateTimePickerEarliest);
            Controls.Add(runCounter);
            Controls.Add(progressLabel);
            Controls.Add(label8);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(resubmitButton);
            Controls.Add(retrieveFailedRunsButton);
            Controls.Add(listView1);
            Controls.Add(logRichTextBox);
            Controls.Add(logicAppRichTextBox);
            Controls.Add(resourceGroupRichTextBox);
            Controls.Add(subscriptionRichTextBox);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximumSize = new Size(816, 633);
            MinimumSize = new Size(816, 633);
            Name = "BulkFailedRunsResubmit";
            Text = "DVS: Logic App Consumption Bulk Failed Runs Resubmit Tool";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox subscriptionRichTextBox;
        private RichTextBox resourceGroupRichTextBox;
        private RichTextBox logicAppRichTextBox;
        private RichTextBox logRichTextBox;
        private ListView listView1;
        private Button retrieveFailedRunsButton;
        private Button resubmitButton;
        private Label label1;
        private Label label2;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label8;
        private Label progressLabel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private Label runCounter;
        private DateTimePicker dateTimePickerEarliest;
        private DateTimePicker dateTimePickerLatest;
    }
}