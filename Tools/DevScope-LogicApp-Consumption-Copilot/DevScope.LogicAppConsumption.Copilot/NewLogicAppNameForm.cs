using System;
using System.Windows.Forms;

namespace DevScope.LogicAppConsumption.Copilot
{
    public partial class NewLogicAppNameForm : Form
    {
        public string AddNewLogicAppName { get; private set; }
        private MainFormAddParam mainForm;

        public NewLogicAppNameForm(MainFormAddParam mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void buttonConfirmNewLAName_Click(object sender, EventArgs e)
        {
            // Capture the new Logic App name entered by the user.
            AddNewLogicAppName = richTextBoxNewLAName.Text;

            // Close the form.
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void richTextBoxNewLAName_TextChanged(object sender, EventArgs e)
        {
            // Handle any text change events if needed.
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
