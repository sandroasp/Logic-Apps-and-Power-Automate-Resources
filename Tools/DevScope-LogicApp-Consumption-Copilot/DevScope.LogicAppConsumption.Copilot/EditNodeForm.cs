using Newtonsoft.Json.Linq;
using System;
using System.Windows.Forms;

namespace DevScope.LogicAppConsumption.Copilot
{
    public partial class EditNodeForm : Form
    {
        private JObject jsonTemplate;
        private JObject jsonTemplate2;
        private JObject jsonTemplate3;
        private MainFormAddParam mainForm;
        public string EditedData
        {
            get { return textBoxEditedData.Text; }
            set { textBoxEditedData.Text = value; }
        }


        public EditNodeForm(MainFormAddParam parentForm)
        {
            InitializeComponent();
            mainForm = parentForm;
            jsonTemplate = mainForm.GetJsonTemplate();
            jsonTemplate2 = mainForm.logicAppParametersJson;
            jsonTemplate3 = mainForm.logicAppParametersTemplateJson;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            // Handle the OK button click action here.
            // Typically, you'd save the edited data.
            // Example: Save this.textBoxEditedData.Text
            // Preserve the existing structure
            //mainForm.RefreshTreeView();
            string newName = this.textBoxEditedData.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // Handle the Cancel button click action here.
            // Typically, you'd discard any changes made.
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void textBoxEditedData_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
