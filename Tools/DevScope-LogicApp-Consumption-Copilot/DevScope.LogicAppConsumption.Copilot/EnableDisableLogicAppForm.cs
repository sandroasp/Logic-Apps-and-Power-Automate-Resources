using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace DevScope.LogicAppConsumption.Copilot
{
    public partial class EnableDisableLogicAppForm : Form
    {
        public string SelectedLogicAppName { get; private set; }
        public string SelectedState { get; private set; }
        private JObject logicAppJson; // Reference to the Logic App JSON data

        public EnableDisableLogicAppForm(List<string> logicAppNames, JObject jsonTemplate)
        {
            InitializeComponent();

            // Populate the ComboBox with Logic App names
            comboBoxLogicApp.Items.AddRange(logicAppNames.ToArray());
            comboBoxLogicApp.SelectedIndex = 0; // Set the default selection.

            this.logicAppJson = jsonTemplate; // Make sure to set the jsonTemplate
            this.StartPosition = FormStartPosition.CenterParent;


        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            // Get the selected Logic App and state
            string selectedComboItem = comboBoxLogicApp.SelectedItem.ToString();
            string[] comboParts = selectedComboItem.Split(new[] { " - " }, StringSplitOptions.None);
            SelectedLogicAppName = comboParts[0];
            SelectedState = radioButtonEnabled.Checked ? "Enabled" : "Disabled";

            // Now, you can update your JSON template based on the selections.
            // For example, you can find the corresponding Logic App and add the "state" property.
            // This is a simplified example; adjust it based on your JSON structure.
            if (logicAppJson != null)
            {
                JArray resourcesArray = logicAppJson["resources"] as JArray;
                if (resourcesArray != null)
                {

                    foreach (JObject resource in resourcesArray)
                    {
                        if (resource["name"]?.ToString() == SelectedLogicAppName)
                        {
                            // Update the state property
                            resource["properties"]["state"] = SelectedState;
                        }
                    }
                }
            }

            // Update the ComboBox item with the new state
            int selectedIndex = comboBoxLogicApp.SelectedIndex;
            comboBoxLogicApp.Items[selectedIndex] = $"{SelectedLogicAppName} - {SelectedState}";

            // Close the form
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void comboBoxLogicApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            // You can add any logic you need when the selected item in the ComboBox changes.
        }

        private void radioButtonEnabled_CheckedChanged(object sender, EventArgs e)
        {
            // You can add any logic you need when the radio button state changes.
        }

        private void radioButtonDisabled_CheckedChanged(object sender, EventArgs e)
        {
            // You can add any logic you need when the radio button state changes.
        }

   

        private void EnableDisableLogicAppForm_Load(object sender, EventArgs e)
        {
            // Clear the ComboBox to avoid duplicate entries
            comboBoxLogicApp.Items.Clear();

            if (logicAppJson != null)
            {
                JArray resourcesArray = logicAppJson["resources"] as JArray;
                if (resourcesArray != null)
                {

                    foreach (JObject resource in resourcesArray)
                    {
                        string resourceName = resource["name"]?.ToString();
                        string resourceState = resource["properties"]?["state"]?.ToString();

                        // Combine the name and state to create the ComboBox item
                        comboBoxLogicApp.Items.Add($"{resourceName} - {resourceState}");
                    }
                }
            }

            // Set the default selection
            comboBoxLogicApp.SelectedIndex = 0;
        }



    }
}
