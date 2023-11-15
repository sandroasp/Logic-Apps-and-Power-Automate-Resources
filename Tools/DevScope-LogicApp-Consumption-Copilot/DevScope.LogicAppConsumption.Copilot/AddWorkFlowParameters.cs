using System;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace DevScope.LogicAppConsumption.Copilot
{
    public partial class AddWorkFlowParameters : Form
    {
        public string NameValue { get; private set; }
        public string ValueValue { get; private set; }
        public string TypeValue { get; private set; }
        private JObject logicAppJson; // Reference to the Logic App JSON data

        private MainFormAddParam mainForm;


        public AddWorkFlowParameters(MainFormAddParam parentForm)
        {
            InitializeComponent();
            mainForm = parentForm;
            logicAppJson = mainForm.GetJsonTemplate();
            // Set the StartPosition to CenterParent
            this.StartPosition = FormStartPosition.CenterParent;
            // Populate the combo box with resource numbers.
            PopulateResourceComboBox();

        }



        private void SubmitButtonWP_Click(object sender, EventArgs e)
        {
            // Get the values from the form's input fields
            NameValue = richTextBoxNameWP.Text;
            ValueValue = richTextBoxValueWP.Text;
            TypeValue = comboBoxTypeWP.Text;

            // Ensure the "parameters" property exists in the JSON data
            if (logicAppJson["resources"]?[comboBoxresourceNumberWP.SelectedIndex]?["properties"]?["parameters"] == null)
            {
                logicAppJson["resources"][comboBoxresourceNumberWP.SelectedIndex]["properties"]["parameters"] = new JObject();
            }

            // Validate parameter values based on their types
            if (!IsValidParameterValue(TypeValue, ValueValue))
            {
                MessageBox.Show("Invalid value for the selected parameter type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create a new parameter object with the selected type
            JObject parameterObject = new JObject();
            if (!string.IsNullOrEmpty(TypeValue))
            {
                parameterObject["type"] = TypeValue;
            }
            parameterObject["value"] = ValueValue;

            // Add the parameter to the "parameters" property within the JSON data
            logicAppJson["resources"][comboBoxresourceNumberWP.SelectedIndex]["properties"]["parameters"][NameValue] = parameterObject;

            // Close the form
            this.DialogResult = DialogResult.OK;
            this.Close();

            mainForm.RefreshTreeView();

            // You may want to update and refresh the TreeView in the MainForm
            // For example: mainForm.RefreshLogicAppParametersTreeView();
        }


        private bool IsValidParameterValue(string parameterType, string parameterValue)
        {
            // Add validation logic here based on the parameter type (e.g., check if it's a valid number for numeric types).
            // Return true if the value is valid; otherwise, return false.
            // You can add more complex validation based on the parameter type if needed.
            return true; // Placeholder, add your validation logic here.
        }

        private void richTextBoxNameWP_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBoxValueWP_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxTypeWP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void resourceNumberWP_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxresourceNumberWP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PopulateResourceComboBox()
        {
            if (logicAppJson != null && logicAppJson["resources"] is JArray resourcesArray)
            {
                for (int i = 0; i < resourcesArray.Count; i++)
                {
                    if (resourcesArray[i]["name"] != null)
                    {
                        string resourceName = $"{i} - {resourcesArray[i]["name"].ToString()}";
                        comboBoxresourceNumberWP.Items.Add(resourceName);
                    }
                }

                if (comboBoxresourceNumberWP.Items.Count > 0)
                {
                    comboBoxresourceNumberWP.SelectedIndex = 0; // Set the default selection.
                }
            }
        }


    }
}