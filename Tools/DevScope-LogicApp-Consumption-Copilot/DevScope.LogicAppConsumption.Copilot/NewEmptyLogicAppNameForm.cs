using System;
using System.Windows.Forms;

namespace DevScope.LogicAppConsumption.Copilot
{
    public partial class NewEmptyLogicAppNameForm : Form
    {
        public string AddNewEmptyLogicAppName { get; private set; }
        private MainFormAddParam mainForm;

        public NewEmptyLogicAppNameForm(MainFormAddParam mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void buttonConfirmNewEmptyLAName_Click(object sender, EventArgs e)
        {
            // Capture the new Logic App name entered by the user.
            AddNewEmptyLogicAppName = richTextBoxNewEmptyLAName.Text;

            // Close the form.
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void richTextBoxNewEmptyLAName_TextChanged(object sender, EventArgs e)
        {
            // Handle any text change events if needed.
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
