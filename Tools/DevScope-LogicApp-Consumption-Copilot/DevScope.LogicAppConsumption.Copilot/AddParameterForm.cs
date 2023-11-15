using System;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace DevScope.LogicAppConsumption.Copilot
{
    public partial class AddParameterForm : Form
    {
        public string NameValue { get; private set; }
        public string ValueValue { get; private set; }

        private JObject jsonTemplate;
        private JObject jsonTemplate2;
        private JObject jsonTemplate3;
        private MainFormAddParam mainForm;



        // Declare a ComboBox for parameter type

        public AddParameterForm(MainFormAddParam parentForm)
        {
            InitializeComponent();
            mainForm = parentForm;
            jsonTemplate = mainForm.GetJsonTemplate();
            jsonTemplate2 = mainForm.logicAppParametersJson;
            jsonTemplate3 = mainForm.logicAppParametersTemplateJson;
            // Set the StartPosition to CenterParent
            this.StartPosition = FormStartPosition.CenterParent;

        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            // Get the selected parameter type from the ComboBox
            string selectedType = comboBoxType.Text;

            // Check if a type is selected
            if (string.IsNullOrEmpty(selectedType))
            {
                MessageBox.Show("Please select a parameter type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the values from the text boxes
            NameValue = richTextBoxName.Text;
            ValueValue = richTextBoxValue.Text;

            addParameterToTreeView(selectedType, NameValue, ValueValue, jsonTemplate, false); // add parameter to treeView
            if (mainForm.checkBoxAddParameters.Checked)            {
                addParameterToTreeView(selectedType, NameValue, ValueValue, jsonTemplate2, true); // add parameter to TreeViewLogicAppParameters
                addParameterToTreeView(selectedType, NameValue, ValueValue, jsonTemplate3, true); // add parameter to TreeViewLogicAppParametersTemplate

                mainForm.RefreshLogicAppParametersTreeView();  // Add this method to refresh the Logic App Parameters TreeView
                mainForm.RefreshLogicAppParametersTemplateTreeView();  // Add this method to refresh the Logic App Parameters Template TreeView

            }
            // Close the form and return DialogResult.OK
            this.DialogResult = DialogResult.OK;
            this.Close();

            // Refresh the TreeView in the MainForm
            mainForm.RefreshTreeView();
           
            
        }

        private void addParameterToTreeView(string selectedType, string NameValue, string ValueValue, JObject jsonTemplate, bool basicParameter) {

            // Ensure the "parameters" property exists in the JSON template
            if (jsonTemplate["parameters"] == null)
            {
                jsonTemplate["parameters"] = new JObject();
            }

            // Validate parameter values based on their types
            if ((selectedType == "Float" || selectedType == "Int") && !IsNumeric(ValueValue))
            {
                MessageBox.Show("Invalid value for the selected parameter type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            JObject parameterValue;
            if (basicParameter)
            {
                // Create a new parameter object with the selected type
                parameterValue = new JObject
                {
                    { "value", ValueValue }
                };
            }
            else
            {
                // Create a new parameter object with the selected type
                parameterValue = new JObject
                {
                    { "type", selectedType },
                    { "value", ValueValue }
                };
            }
          

            JProperty newParameter = new JProperty(NameValue, parameterValue);

            // Preserve the existing structure
            JObject existingParameters = jsonTemplate["parameters"] as JObject;

            if (existingParameters == null)
            {
                existingParameters = new JObject();
                jsonTemplate["parameters"] = existingParameters;
            }

            existingParameters.Add(newParameter);
        }

       
        private bool IsNumeric(string value)
        {
            // This function checks if a string is a valid number (integer or float).
            float result;
            return float.TryParse(value, out result);
        }


        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddParameterForm_Load(object sender, EventArgs e)
        {

        }
    }
}
