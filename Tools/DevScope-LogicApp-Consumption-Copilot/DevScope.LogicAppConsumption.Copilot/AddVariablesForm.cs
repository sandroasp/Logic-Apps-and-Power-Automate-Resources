using System;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace DevScope.LogicAppConsumption.Copilot
{
    public partial class AddVariablesForm : Form
    {
        private JObject jsonTemplate;
        private MainFormAddParam mainForm;

        public AddVariablesForm()
        {
            InitializeComponent();
        }

        public AddVariablesForm(MainFormAddParam parentForm, JObject template)
        {
            InitializeComponent();
            mainForm = parentForm;
            jsonTemplate = template; // Receive the JSON template from the main form
                                     // Set the StartPosition to CenterParent
            this.StartPosition = FormStartPosition.CenterParent;
        }

        public JObject GetUpdatedJsonTemplate()
        {
            return jsonTemplate; // Return the updated JSON template with variables
        }

        private void AddVariableButton_Click(object sender, EventArgs e)
        {
            // Get the values from the form
            string variableName = richTextBoxInitVariableName.Text;
            string variableValue = richTextBoxInitVariableValue.Text;

            // Perform validation if needed
            if (string.IsNullOrEmpty(variableName))
            {
                MessageBox.Show("Please enter a variable name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create a new variable with only a name and a value
            JProperty newVariable = new JProperty(variableName, variableValue);

            // Ensure the "variables" property exists in the JSON template
            if (jsonTemplate["variables"] == null)
            {
                jsonTemplate["variables"] = new JObject();
            }

            // Add the new variable to the JSON template
            JObject existingVariables = jsonTemplate["variables"] as JObject;
            if (existingVariables == null)
            {
                existingVariables = new JObject();
                jsonTemplate["variables"] = existingVariables;
            }
            existingVariables.Add(newVariable);

            // Close the form and return DialogResult.OK
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
